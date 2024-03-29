﻿using System.Collections.Generic;
using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Email;
using LT.DigitalOffice.Models.Broker.Requests.TextTemplate;

namespace LT.DigitalOffice.EmailService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string SendEmailEndpoint { get; set; }
    public string CreateSmtpCredentialsEndpoint { get; set; }
    public Dictionary<string, string> FindUserParseEntitiesEndpoint { get; set; }

    [AutoInjectRequest(typeof(IGetSmtpCredentialsRequest))]
    public string GetSmtpCredentialsEndpoint { get; set; }

    //TextTemplate
    [AutoInjectRequest(typeof(IGetTextTemplateRequest))]
    public string GetTextTemplateEndpoint { get; set; }
  }
}
