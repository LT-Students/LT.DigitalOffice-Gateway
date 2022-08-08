using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces
{
  [AutoInject]
  public interface ICreateFeedbackCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateFeedbackRequest request);
  }
}
