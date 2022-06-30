using System.Net.Mail;
using FluentValidation;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting.Interfaces;


namespace LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting
{
  public class CheckSmtpRequestValidator : AbstractValidator<CheckSmtpRequest>, ICheckSmtpRequestValidator
  {
    public CheckSmtpRequestValidator()
    {
      RuleFor(request => request.SmtpInfo)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Smtp information can't be null.")
        .DependentRules(() =>
        {
          RuleFor(request => request.SmtpInfo.Host)
            .NotEmpty().WithMessage("Host can't be empty.");

          RuleFor(request => request.SmtpInfo.Port)
            .NotNull().WithMessage("Port can't be empty.");

          RuleFor(request => request.SmtpInfo.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email can't be empty.")
            .Must(x =>
            {
              try
              {
                MailAddress address = new(x.Trim());
                return true;
              }
              catch
              {
                return false;
              }
            })
              .WithMessage("Incorrect email address.");

          RuleFor(request => request.SmtpInfo.Password)
            .NotEmpty().WithMessage("Password can't be empty.");
        });

      RuleFor(request => request.AdminEmail)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Email can't be empty.")
        .Must(x =>
        {
          try
          {
            MailAddress address = new(x.Trim());
            return true;
          }
          catch
          {
            return false;
          }
        })
        .WithMessage("Incorrect email address.");
    }
  }
}
