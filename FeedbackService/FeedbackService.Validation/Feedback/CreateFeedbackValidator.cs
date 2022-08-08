using FluentValidation;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;

namespace LT.DigitalOffice.FeedbackService.Validation.Feedback
{
  public class CreateFeedbackValidator : AbstractValidator<CreateFeedbackRequest>, ICreateFeedbackValidator
  {
    public CreateFeedbackValidator()
    {
      CascadeMode = CascadeMode.Stop;

      RuleFor(f => f.Content)
        .NotEmpty().WithMessage("Content cannot be empty.")
        .MaximumLength(1000).WithMessage("Content length must be less than 1000 characters");

      RuleFor(f => f.Type)
        .IsInEnum();
    }
  }
}
