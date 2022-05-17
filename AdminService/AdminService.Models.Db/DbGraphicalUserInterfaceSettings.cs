using System;

namespace LT.DigitalOffice.AdminService.Models.Db
{
  public class DbGraphicalUserInterfaceSetting
  {
    public const string TableName = "GraphicalUserInterfaceSettings";

    public Guid Id { get; set; }
    public string PortalName { get; set; }
    public string LogoContent { get; set; }
    public string LogoExtension { get; set; }
    public string SiteUrl { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAtUtc { get; set; }
  }
}
