using System;

namespace LT.DigitalOffice.AdminService.Models.Dto.Models
{
  public class GuiInfo
  {
    public Guid? Id { get; set; }
    public string PortalName { get; set; }
    public string SiteUrl { get; set; }
    public ImageConsist Logo { get; set; }
    public ImageConsist Favicon { get; set; }
<<<<<<< HEAD
    public DateTime CreatedAtUtc { get; set;}
=======
    public DateTime? CreatedAtUtc { get; set; }
>>>>>>> 7cf26242f4d5cf9b25923cdc69d4d9637407af7b
  }
}
