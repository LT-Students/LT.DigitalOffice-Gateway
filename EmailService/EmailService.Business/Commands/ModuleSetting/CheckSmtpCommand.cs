using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Broker.Helpers;
using LT.DigitalOffice.EmailService.Broker.Requests.Interfaces;
using LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting.Interfaces;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Responses.TextTemplate;

namespace LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting
{
  public class CheckSmtpCommand : ICheckSmtpCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICheckSmtpRequestValidator _validator;
    private readonly ISmtpSettingsRepository _repository;
    private readonly EmailSender _emailSender;
    private readonly ITextTemplateService _textTemplateService;

    private async Task<bool> SendCheckAsync(string email, string locale, List<string> errors, SmtpInfo smtpInfo)
    {
      IGetTextTemplateResponse textTemplate = await _textTemplateService.GetAsync(TemplateType.SmtpCheck, locale, errors);

      if (textTemplate is null)
      {
        return false;
      }

      return await _emailSender.SendSmtpCheckAsync(receiver: email, subject: textTemplate.Subject, text: textTemplate.Text, smtpInfo: smtpInfo);
    }

    public CheckSmtpCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICheckSmtpRequestValidator validator,
      ISmtpSettingsRepository repository,
      EmailSender emailSender,
      ITextTemplateService textTemplateService)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _repository = repository;
      _emailSender = emailSender;
      _textTemplateService = textTemplateService;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(CheckSmtpRequest request)
    {
      bool isFirstCheck = !await _repository.DoesExistAnyAsync();

      if (!isFirstCheck && !await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      return await SendCheckAsync(
          email: request.AdminEmail,
          locale: "ru",
          errors: errors,
          smtpInfo: request.SmtpInfo)
        ? new OperationResultResponse<bool>(body: true)
        : _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
    }
  }
}
