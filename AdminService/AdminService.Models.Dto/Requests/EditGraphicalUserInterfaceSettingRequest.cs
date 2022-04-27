using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Models.Dto.Requests;
public record EditGraphicalUserInterfaceSettingRequest
{
  public string PortalName { get; set; }
  public string SiteUrl { get; set; }
  public ImageConsist Logo { get; set; }
}

