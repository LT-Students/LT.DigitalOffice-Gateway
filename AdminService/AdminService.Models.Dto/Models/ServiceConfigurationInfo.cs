using System;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public record ServiceConfigurationInfo
  {
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public bool IsActive { get; set; }
  }
}
