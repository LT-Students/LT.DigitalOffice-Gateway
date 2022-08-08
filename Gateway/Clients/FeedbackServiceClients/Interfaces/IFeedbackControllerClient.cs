using System;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients.Interfaces
{
  [AutoInject]
  public interface IFeedbackControllerClient
  {
    Task<FindResultResponse<FeedbackInfo>> FindAsync(FindFeedbacksFilter filter);
    Task<OperationResultResponse<Guid?>> CreateAsync(CreateFeedbackRequest request);
  }
}
