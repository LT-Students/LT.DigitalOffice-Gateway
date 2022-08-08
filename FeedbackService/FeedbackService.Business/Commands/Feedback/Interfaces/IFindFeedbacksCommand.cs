using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces
{
  [AutoInject]
  public interface IFindFeedbacksCommand
  {
    Task<FindResultResponse<FeedbackInfo>> ExecuteAsync(FindFeedbacksFilter filter);
  }
}
