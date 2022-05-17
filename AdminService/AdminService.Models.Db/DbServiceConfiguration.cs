using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.AdminService.Models.Db
{
  public class DbServiceConfiguration
  {
    public const string TableName = "ServicesConfigurations";

    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public bool IsActive { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
  }
}
