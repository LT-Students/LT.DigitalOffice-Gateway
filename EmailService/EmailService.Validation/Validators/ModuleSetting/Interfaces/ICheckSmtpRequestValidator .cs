using FluentValidation;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EmailService.Validation.Validators.ModuleSetting.Interfaces
{
  [AutoInject]
  public interface ICheckSmtpRequestValidator : IValidator<CheckSmtpRequest>
  {
  }
}
