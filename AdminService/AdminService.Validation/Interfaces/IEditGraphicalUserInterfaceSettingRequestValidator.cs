using FluentValidation;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.AdminService.Validation.Interfaces;
[AutoInject]
public interface IEditGraphicalUserInterfaceSettingRequestValidator
  : IValidator<JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest>>
{
}

