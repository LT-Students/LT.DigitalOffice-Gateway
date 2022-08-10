using System;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients.Interfaces
{
  [AutoInject]
  public interface IFeedbackControllerClient
  {
    Task<OperationResultResponse<Guid?>> CreateAsync(CreateFeedbackRequest request);
  }
}
