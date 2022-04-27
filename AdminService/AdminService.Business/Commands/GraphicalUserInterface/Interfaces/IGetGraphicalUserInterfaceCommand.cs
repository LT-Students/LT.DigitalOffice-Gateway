using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface.Interfaces
{
  [AutoInject]
  public interface IGetGraphicalUserInterfaceCommand
  {
    Task<OperationResultResponse<GuiInfo>> ExecuteAsync();
  }
}
