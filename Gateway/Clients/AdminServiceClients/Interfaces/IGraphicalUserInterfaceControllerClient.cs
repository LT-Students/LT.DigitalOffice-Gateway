using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.Gateway.Clients.AdminServiceClients.Interfaces
{
  [AutoInject]
  public interface IGraphicalUserInterfaceControllerClient
  {
    Task<OperationResultResponse<GuiInfo>> GetAsync();

    Task<OperationResultResponse<bool>> EditAsync(JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request);
  }
}
