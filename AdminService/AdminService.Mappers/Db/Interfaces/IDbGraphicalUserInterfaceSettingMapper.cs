using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbGraphicalUserInterfaceSettingMapper
  {
    DbGraphicalUserInterfaceSetting Map(CreateGuiRequest request);
  }
}
