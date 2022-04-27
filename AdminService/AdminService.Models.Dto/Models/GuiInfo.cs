using System;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public class GuiInfo
  {
    public Guid? Id { get; set; }
    public string PortalName { get; set; }
    public string SiteUrl { get; set; }
    public ImageConsist Logo { get; set; }
  }
}
