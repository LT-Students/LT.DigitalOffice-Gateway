using System;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.Gateway.Clients.EmailServiceClients.Interfaces
{
  [AutoInject]
  public interface IModuleSettingControllerClient
  {
    Task<OperationResultResponse<bool>> EditAsync(Guid moduleSettingId, JsonPatchDocument<EditModuleSettingRequest> patch);
  }
}
