using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbGraphicalUserInterfaceSettingMapper
  {
    DbGraphicalUserInterfaceSetting Map(GuiInfo request);
  }
}
