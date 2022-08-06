using LT.DigitalOffice.AdminService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;

namespace LT.DigitalOffice.AdminService.Mappers.Responses
{
  public class GuiInfoMapper : IGuiInfoMapper
  {
    public GuiInfo Map(DbGraphicalUserInterfaceSetting dbGui)
    {
      if (dbGui is null)
      {
        return default;
      }

      return new()
      {
        Id = dbGui.Id,
        PortalName = dbGui.PortalName,
        Logo = new ImageConsist 
        {
          Content = dbGui.LogoContent,
          Extension = dbGui.LogoExtension
        },
        Favicon = new ImageConsist
        {
          Content = dbGui.FaviconContent,
          Extension = dbGui.FaviconExtension
        },
        SiteUrl = dbGui.SiteUrl
      };
    }
  }
}
