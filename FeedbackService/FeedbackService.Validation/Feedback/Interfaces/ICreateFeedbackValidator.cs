using FluentValidation;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces
{
  [AutoInject]
  public interface ICreateFeedbackValidator : IValidator<CreateFeedbackRequest>
  {
  }
}
