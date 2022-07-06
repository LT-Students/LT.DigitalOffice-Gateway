using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Broker.Helpers;
using LT.DigitalOffice.EmailService.Broker.Requests.Interfaces;
using LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting.Interfaces;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Configurations;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.TextHandlers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Responses.TextTemplate;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;


namespace LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting
{
  public class CheckSmtpCommand : ICheckSmtpCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICheckSmtpRequestValidator _validator;
    private readonly ISmtpSettingsRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IOptions<MemoryCacheConfig> _cacheOptions;
    private readonly EmailSender _emailSender;
    private readonly ITextTemplateService _textTemplateService;
    private readonly ITextTemplateParser _parser;

    private int GenerateCode()
    {
      Random rnd = new Random();
      return rnd.Next(10000, 100000);
    }

    private async Task<bool> SendCodeAsync(string email, string locale, List<string> errors, string code, SmtpInfo smtpInfo)
    {
      //TODO change Greeting to SmtpCheck
      IGetTextTemplateResponse textTemplate = await _textTemplateService.GetAsync(TemplateType.Greeting, locale, errors);

      if (textTemplate is null)
      {
        return false;
      }

      string parsedText = _parser.Parse(
        new Dictionary<string, string> { { "Password", code } },
        textTemplate.Text);

      return await _emailSender.SendEmailAsync(receiver: email, subject: textTemplate.Subject, text: parsedText, smtpInfo: smtpInfo);
    }

    public CheckSmtpCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICheckSmtpRequestValidator validator,
      ISmtpSettingsRepository repository,
      IMemoryCache cache,
      IOptions<MemoryCacheConfig> cacheOptions,
      EmailSender emailSender,
      ITextTemplateService textTemplateService,
      ITextTemplateParser parser)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _repository = repository;
      _cache = cache;
      _cacheOptions = cacheOptions;
      _emailSender = emailSender;
      _textTemplateService = textTemplateService;
      _parser = parser;
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

      string casheValue = isFirstCheck ? request.GetHashCode().ToString() : request.SmtpInfo.GetHashCode().ToString();
      string code = GenerateCode().ToString();

      _cache.Set(key: code, value: casheValue, TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes));

      OperationResultResponse<bool> response = new(body:
        await SendCodeAsync(
          email: request.AdminEmail,
          locale: "ru",
          errors: errors,
          code: code,
          smtpInfo: request.SmtpInfo));

      return response.Body
        ? response
        : _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, response.Errors);
    }
  }
}
