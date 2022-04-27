﻿namespace LT.DigitalOffice.EmailService.Models.Dto.Configurations
{
  public class EmailEngineConfig
  {
    public const string SectionName = "EmailEngineConfig";

    public int ResendIntervalInMinutes { get; set; }
    public int MaxResendingCount { get; set; }
  }
}
