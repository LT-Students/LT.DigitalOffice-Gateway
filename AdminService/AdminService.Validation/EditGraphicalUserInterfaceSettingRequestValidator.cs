using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using LT.DigitalOffice.Kernel.Validators;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;

namespace LT.DigitalOffice.AdminService.Validation;
public class EditGraphicalUserInterfaceSettingRequestValidator
  : BaseEditRequestValidator<EditGraphicalUserInterfaceSettingRequest>,
  IEditGraphicalUserInterfaceSettingRequestValidator
{
  private readonly IImageContentValidator _imageContentValidator;
  private readonly IImageExtensionValidator _imageExtensionValidator;

  private void HandleInternalPropertyValidation(Operation<EditGraphicalUserInterfaceSettingRequest> requestedOperation, CustomContext context)
  {
    Context = context;
    RequestedOperation = requestedOperation;

    #region paths

    AddСorrectPaths(
      new List<string>
      {
        nameof(EditGraphicalUserInterfaceSettingRequest.PortalName),
        nameof(EditGraphicalUserInterfaceSettingRequest.SiteUrl),
        nameof(EditGraphicalUserInterfaceSettingRequest.Logo),
        nameof(EditGraphicalUserInterfaceSettingRequest.Favicon)
      });

    AddСorrectOperations(
      nameof(EditGraphicalUserInterfaceSettingRequest.PortalName),
      new List<OperationType> { OperationType.Replace });

    AddСorrectOperations(
      nameof(EditGraphicalUserInterfaceSettingRequest.SiteUrl),
      new List<OperationType> { OperationType.Replace });

    AddСorrectOperations(
      nameof(EditGraphicalUserInterfaceSettingRequest.Logo),
      new List<OperationType> { OperationType.Replace });

    AddСorrectOperations(
      nameof(EditGraphicalUserInterfaceSettingRequest.Favicon),
      new List<OperationType> { OperationType.Replace });

    #endregion

    #region SiteUrl

    AddFailureForPropertyIf(
      nameof(EditGraphicalUserInterfaceSettingRequest.SiteUrl),
      x => x == OperationType.Replace,
      new Dictionary<Func<Operation<EditGraphicalUserInterfaceSettingRequest>, bool>, string>
      {
        { x => !string.IsNullOrEmpty(x.value?.ToString().Trim()), "Site url must not be empty." }
      });

    #endregion

    #region Logo

    AddFailureForPropertyIf(
      nameof(EditGraphicalUserInterfaceSettingRequest.Logo),
      x => x == OperationType.Replace,
      new Dictionary<Func<Operation<EditGraphicalUserInterfaceSettingRequest>, bool>, string>
      {
        { x =>
          {
            ImageConsist logo = JsonConvert.DeserializeObject<ImageConsist>(x.value?.ToString());

            return logo is null
              ? true
              : _imageContentValidator.Validate(logo.Content).IsValid &&
                _imageExtensionValidator.Validate(logo.Extension).IsValid;
          },
          "Wrong logo type." },
      });

    #endregion

    #region Favicon

    // TO DO: when there will be desigion about favicon's extension and size, fix validation according this desigion
    AddFailureForPropertyIf(
      nameof(EditGraphicalUserInterfaceSettingRequest.Favicon),
      x => x == OperationType.Replace,
      new Dictionary<Func<Operation<EditGraphicalUserInterfaceSettingRequest>, bool>, string>
      {
        { x =>
          {
            ImageConsist icon = JsonConvert.DeserializeObject<ImageConsist>(x.value?.ToString());

            return icon is null
              ? true
              : _imageContentValidator.Validate(icon.Content).IsValid &&
                (icon.Extension == ".png" || icon.Extension == ".svg" || icon.Extension == ".ico");
          },
          "Wrong favicon type." },
      });

    #endregion
  }

  public EditGraphicalUserInterfaceSettingRequestValidator(
    IImageContentValidator imageContentValidator,
    IImageExtensionValidator imageExtensionValidator)
  {
    _imageContentValidator = imageContentValidator;
    _imageExtensionValidator = imageExtensionValidator;

    RuleForEach(x => x.Operations)
      .Custom(HandleInternalPropertyValidation);
  }
}
