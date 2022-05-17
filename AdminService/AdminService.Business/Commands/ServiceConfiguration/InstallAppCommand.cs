using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Db.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Requests.Email;
using LT.DigitalOffice.Models.Broker.Requests.User;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration
{
  public class InstallAppCommand : IInstallAppCommand
  {
    private readonly ILogger<IInstallAppCommand> _logger;
    private readonly IInstallAppRequestValidator _validator;
    private readonly IDbGraphicalUserInterfaceSettingMapper _guiMapper;
    private readonly IServiceConfigurationRepository _configurationRepository;
    private readonly IGraphicalUserInterfaceSettingRepository _guiRepository;
    private readonly IRequestClient<ICreateAdminRequest> _rcCreateAdmin;
    private readonly IRequestClient<ICreateSmtpCredentialsRequest> _rcCreateSmtp;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;

    private async Task<bool> CreateSmtp(SmtpInfo smtpInfo, List<string> errors)
    {
      return (await RequestHandler.ProcessRequest<ICreateSmtpCredentialsRequest, bool>(
        _rcCreateSmtp,
        ICreateSmtpCredentialsRequest.CreateObj(
          host: smtpInfo.Host,
          port: smtpInfo.Port,
          enableSsl: smtpInfo.EnableSsl,
          email: smtpInfo.Email,
          password: smtpInfo.Password),
        errors,
        _logger));
    }

    private async Task<bool> CreateAdmin(AdminInfo adminInfo, List<string> errors)
    {
      return (await RequestHandler.ProcessRequest<ICreateAdminRequest, bool>(
        _rcCreateAdmin,
        ICreateAdminRequest.CreateObj(
          firstName: adminInfo.FirstName,
          middleName: adminInfo.MiddleName,
          lastName: adminInfo.LastName,
          email: adminInfo.Email,
          login: adminInfo.Login,
          password: adminInfo.Password),
        errors,
        _logger));
    }

    public InstallAppCommand(
      ILogger<IInstallAppCommand> logger,
      IInstallAppRequestValidator validator,
      IDbGraphicalUserInterfaceSettingMapper guiMapper,
      IServiceConfigurationRepository configurationRepository,
      IGraphicalUserInterfaceSettingRepository guiRepository,
      IRequestClient<ICreateAdminRequest> rcCreateAdmin,
      IRequestClient<ICreateSmtpCredentialsRequest> rcCreateSmtp,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator)
    {
      _logger = logger;
      _validator = validator;
      _guiMapper = guiMapper;
      _configurationRepository = configurationRepository;
      _guiRepository = guiRepository;
      _rcCreateAdmin = rcCreateAdmin;
      _rcCreateSmtp = rcCreateSmtp;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(InstallAppRequest request)
    {
      if (await _configurationRepository.DoesAppInstalledAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          new List<string>() { "The app is already installed." });
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      if (!(await CreateSmtp(request.SmtpInfo, errors) &&
        await CreateAdmin(request.AdminInfo, errors)))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      OperationResultResponse<bool> response = new();

      await _guiRepository.CreateAsync(_guiMapper.Map(request.GuiInfo));
      int countDisabledServices = await _configurationRepository.InstallAppAsync(request.ServicesToDisable);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      if (request.ServicesToDisable.Count != countDisabledServices)
      {
        response.Status = OperationResultStatusType.PartialSuccess;
        response.Errors = new List<string>() { "not all services have been disabled." };
      }
      else
      {
        response.Body = true;
      }

      return response;
    }
  }
}
