using System;
using LT.DigitalOffice.AdminService.Mappers.Db.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;

namespace LT.DigitalOffice.AdminService.Mappers.Db
{
  public class DbGraphicalUserInterfaceSettingMapper : IDbGraphicalUserInterfaceSettingMapper
  {
    public DbGraphicalUserInterfaceSetting Map(CreateGuiRequest request)
    {
      if (request is null)
      {
        return null;
      }

      return new()
      {
        Id = Guid.NewGuid(),
        PortalName = request.PortalName,
        LogoContent = request.Logo?.Content,
        LogoExtension = request.Logo?.Extension,
        FaviconContent = request.Favicon?.Content,
        FaviconExtension = request.Favicon?.Extension,
        SiteUrl = request.SiteUrl,
        CreatedAtUtc = DateTime.UtcNow
      };
    }
  }
}
