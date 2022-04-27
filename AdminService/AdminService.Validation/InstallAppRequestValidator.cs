using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentValidation;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.AdminService.Validation
{
  public class InstallAppRequestValidator : AbstractValidator<InstallAppRequest>, IInstallAppRequestValidator
  {
    private static Regex NameRegex = new(@"^([a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+|[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+[-|']?[a-zA-Zа-яА-ЯёЁ]+)$");
    private static Regex PasswordRegex = new(@"(?=.*[.,:;?!*+%\-<>@[\]{}/\\_{}$#])");

    public InstallAppRequestValidator(
      IImageContentValidator imageContentValidator,
      IImageExtensionValidator imageExtensionValidator)
    {
      RuleFor(request => request.AdminInfo)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Admin information can't be null.")
        .DependentRules(() =>
        {
          RuleFor(r => r.AdminInfo.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MaximumLength(45).WithMessage("First name is too long.")
            .Must(x => NameRegex.IsMatch(x.Trim()))
            .WithMessage("First name contains invalid characters.");

          RuleFor(request => request.AdminInfo.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MaximumLength(45).WithMessage("Last name is too long.")
            .Must(x => NameRegex.IsMatch(x.Trim()))
            .WithMessage("Last name contains invalid characters.");

          When(
            request => !string.IsNullOrEmpty(request.AdminInfo.MiddleName),
            () =>
              RuleFor(request => request.AdminInfo.MiddleName)
                .MaximumLength(45).WithMessage("Middle name is too long.")
                .Must(x => NameRegex.IsMatch(x.Trim()))
                .WithMessage("Middle name contains invalid characters."));

          RuleFor(request => request.AdminInfo.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .Must(x => x.Trim().Length > 7).WithMessage("Password is too short.")
            .Must(x => x.Trim().Length < 15).WithMessage("Password is too long.")
            .Must(x => PasswordRegex.IsMatch(x))
            .WithMessage("The password must contain at least one special character.")
            .Must(x => !x.Contains(' ')).WithMessage("Password must not contain space.");

          RuleFor(request => request.AdminInfo.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login can't be empty.")
            .Must(x => char.IsLetter(x[0])).WithMessage("Login must start with a letter.")
            .MinimumLength(3).WithMessage("Login is too short.")
            .MaximumLength(15).WithMessage("Login is too long.")
            .Must(x => x.All(char.IsLetterOrDigit))
            .WithMessage("Login must contain only letters or digits.");

          RuleFor(request => request.AdminInfo.Email)
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
        });

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

      RuleFor(request => request.GuiInfo)
        .Cascade(CascadeMode.Stop)
        .NotNull().WithMessage("Gui information can't be null.")
        .DependentRules(() =>
        {
          RuleFor(r => r.GuiInfo.SiteUrl)
            .NotEmpty().WithMessage("First name cannot be empty.");

          When(r => r.GuiInfo.Logo is not null, () =>
          {
            RuleFor(r => r.GuiInfo.Logo.Content)
              .SetValidator(imageContentValidator);

            RuleFor(r => r.GuiInfo.Logo.Extension)
              .SetValidator(imageExtensionValidator);
          });
        });

      RuleFor(request => request.ServicesToDisable)
        .NotNull().WithMessage("Service to disable can not be null");
    }
  }
}
