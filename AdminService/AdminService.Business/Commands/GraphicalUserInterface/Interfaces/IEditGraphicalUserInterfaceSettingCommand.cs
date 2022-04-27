using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface.Interfaces;

[AutoInject]
public interface IEditGraphicalUserInterfaceSettingCommand
{
  Task<OperationResultResponse<bool>> ExecuteAsync(
    JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request);
}

