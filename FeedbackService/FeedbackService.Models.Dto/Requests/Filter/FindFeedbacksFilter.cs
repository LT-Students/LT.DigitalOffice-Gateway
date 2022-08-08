using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter
{
  public record FindFeedbacksFilter : BaseFindFilter
  {
    [FromQuery(Name = "feedbackstatus")]
    public FeedbackStatusType? FeedbackStatus { get; set; }

    [FromQuery(Name = "feedbacktype")]
    public FeedbackType? FeedbackType { get; set; }
  }
}
