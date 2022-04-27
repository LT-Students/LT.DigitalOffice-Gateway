using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IGuiInfoMapper
  {
    GuiInfo Map(DbGraphicalUserInterfaceSetting dbGui);
  }
}
