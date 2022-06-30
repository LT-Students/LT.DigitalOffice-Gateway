using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Broker.Helpers;
using LT.DigitalOffice.EmailService.Broker.Requests.Interfaces;
using LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting.Interfaces;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Configurations;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
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
    private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks;
    private readonly EmailSender _emailSender;
    private readonly ITextTemplateService _textTemplateService;

    private int GenerateCode()
    {
      Random rnd = new Random();
      return rnd.Next(10000, 100000);
    }

    /*private async Task<bool> SendCodeAsync(string email, string locale, List<string> errors)
    {

      IGetTextTemplateResponse textTemplate = await _textTemplateService
        .GetAsync(TemplateType.Greeting, locale, errors);

      if (textTemplate is null)
      {
        return;
      }
    }*/
    /*    private async Task NotifyAsync(
      DbUser dbUser,
      string password,
      string locale,
      List<string> errors)
    {
      IGetTextTemplateResponse textTemplate = await _textTemplateService
        .GetAsync(TemplateType.Greeting, locale, errors);

      if (textTemplate is null)
      {
        return;
      }

      string parsedText = _parser.Parse(
        new Dictionary<string, string> { { "Password", password } },
        _parser.ParseModel<DbUser>(dbUser, textTemplate.Text));

      string email = dbUser.Communications
        .FirstOrDefault(c => c.Type == (int)CommunicationType.Email)?.Value;

      await _emailService.SendAsync(email, textTemplate.Subject, parsedText, errors);
    }*/

    public CheckSmtpCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICheckSmtpRequestValidator validator,
      ISmtpSettingsRepository repository,
      IMemoryCache cache,
      IOptions<MemoryCacheConfig> cacheOptions,
      ConcurrentDictionary<object, SemaphoreSlim> locks,
      EmailSender emailSender)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _repository = repository;
      _cache = cache;
      _cacheOptions = cacheOptions;
      _locks = locks;
      _emailSender = emailSender;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(CheckSmtpRequest request)
    {
      bool isFirstCheck = !(await _repository.DoesExistAnyAsync());

      if (!isFirstCheck && !await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      if (!_validator.ValidateCustom(request,out List<string> errors))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }

      int casheValue = isFirstCheck ? request.GetHashCode() : request.SmtpInfo.GetHashCode();
      int code = GenerateCode();

      SemaphoreSlim locked = _locks.GetOrAdd(key: code, k => new SemaphoreSlim(1, 1));
      await locked.WaitAsync();

      try
      {
        _cache.Set(key: code, value: casheValue, TimeSpan.FromMinutes(_cacheOptions.Value.CacheLiveInMinutes));
      }
      catch (Exception ex)
      {
        errors.Add(ex.ToString());
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
      }
      finally
      {
        locked.Release();
      }

      bool isSuccess = await _emailSender.SendEmailAsync(receiver: "a", subject: "b", text: "c");  //!!!!!!!!!!

      return new()
      {
        Status = isSuccess ? OperationResultStatusType.FullSuccess : OperationResultStatusType.Failed,
        Body = isSuccess
      };

    }
  }
}
