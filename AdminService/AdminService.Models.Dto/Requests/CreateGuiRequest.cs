using System;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Models.Dto.Requests
{
  public class CreateGuiRequest
  {
    public Guid? Id { get; set; }
    public string PortalName { get; set; }
    public string SiteUrl { get; set; }
    public ImageConsist Logo { get; set; }
    public ImageConsist Favicon { get; set; }
  }
}
