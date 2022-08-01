using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Responses.TextTemplate;

namespace LT.DigitalOffice.EmailService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface ITextTemplateService
  {
    Task<IGetTextTemplateResponse> GetAsync(
      TemplateType templateType,
      string locale,
      List<string> errors,
      Guid? endpointId = null);
  }
}
