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
      RuleFor(request => request.SmtpInfo.Email)
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

      RuleFor(request => request.AdminEmail)
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
