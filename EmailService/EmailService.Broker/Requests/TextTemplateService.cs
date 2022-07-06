using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Broker.Requests.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.TextTemplate;
using LT.DigitalOffice.Models.Broker.Responses.TextTemplate;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.EmailService.Broker.Requests
{
  public class TextTemplateService : ITextTemplateService
  {
    private readonly IRequestClient<IGetTextTemplateRequest> _rcGetTextTemplate;
    private readonly ILogger<TextTemplateService> _logger;

    public TextTemplateService(
      IRequestClient<IGetTextTemplateRequest> rcGetTextTemplate,
      ILogger<TextTemplateService> logger)
    {
      _rcGetTextTemplate = rcGetTextTemplate;
      _logger = logger;
    }

    public async Task<IGetTextTemplateResponse> GetAsync(
      TemplateType templateType,
      string locale,
      List<string> errors,
      Guid? endpointId = null)
    {
      return await RequestHandler.ProcessRequest<IGetTextTemplateRequest, IGetTextTemplateResponse>(
        _rcGetTextTemplate,
        IGetTextTemplateRequest.CreateObj(
          endpointId: endpointId,
          templateType: templateType,
          locale: locale),
        errors,
        _logger);
    }
  }
}
