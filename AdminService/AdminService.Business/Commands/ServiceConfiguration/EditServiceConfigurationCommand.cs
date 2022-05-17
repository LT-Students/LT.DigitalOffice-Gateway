using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration
{
  public class EditServiceConfigurationCommand : IEditServiceConfigurationCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IServiceConfigurationRepository _repository;
    private readonly IResponseCreator _responseCreator;

    public EditServiceConfigurationCommand(
      IAccessValidator accessValidator,
      IServiceConfigurationRepository repository,
      IResponseCreator responseCreator)
    {
      _accessValidator = accessValidator;
      _repository = repository;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(List<Guid> servicesIds)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (servicesIds is null || !servicesIds.Any())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest);
      }

      List<Guid> changedServicesIds = await _repository.EditAsync(servicesIds);

      bool difference = changedServicesIds.Count == servicesIds.Count;

      return new OperationResultResponse<bool>()
      {
        Status = difference ? OperationResultStatusType.FullSuccess : OperationResultStatusType.PartialSuccess,
        Body = true,
        Errors = difference ? null : new() { "Request contains incorrect services Ids." }
      };
    }
  }
}
