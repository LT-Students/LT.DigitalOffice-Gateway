using System;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.Gateway.Clients.EmailServiceClients.Interfaces
{
  public interface IUnsentEmailController
  {
    Task<OperationResultResponse<bool>> ResendAsync(Guid unsentEmailId);

    Task<FindResultResponse<UnsentEmailInfo>> FindAsync(BaseFindFilter filter);
  }
}
