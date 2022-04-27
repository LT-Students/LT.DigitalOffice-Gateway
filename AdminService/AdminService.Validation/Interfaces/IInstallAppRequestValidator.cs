using FluentValidation;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AdminService.Validation.Interfaces
{
  [AutoInject]
  public interface IInstallAppRequestValidator : IValidator<InstallAppRequest>
  {
  }
}
