using System;
using System.Collections.Generic;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Models.Dto.Requests
{
  public record InstallAppRequest
  {
    public SmtpInfo SmtpInfo { get; set; }
    public AdminInfo AdminInfo { get; set; }
    public CreateGuiRequest CreateGuiRequest { get; set; }
    public List<Guid> ServicesToDisable { get; set; }
}
}
