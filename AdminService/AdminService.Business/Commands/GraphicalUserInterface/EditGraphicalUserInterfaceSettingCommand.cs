using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.AdminService.Validation.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.AdminService.Business.Commands.GraphicalUserInterface;

public class EditGraphicalUserInterfaceSettingCommand : IEditGraphicalUserInterfaceSettingCommand
{
  private readonly IAccessValidator _accessValidator;
  private readonly IEditGraphicalUserInterfaceSettingRequestValidator _validator;
  private readonly IPatchGraphicalUserInterfaceSettingMapper _mapper;
  private readonly IGraphicalUserInterfaceSettingRepository _repository;
  private readonly IResponseCreator _responseCreator;

  public EditGraphicalUserInterfaceSettingCommand(
    IAccessValidator accessValidator,
    IEditGraphicalUserInterfaceSettingRequestValidator validator,
    IPatchGraphicalUserInterfaceSettingMapper mapper,
    IGraphicalUserInterfaceSettingRepository repository,
    IResponseCreator responseCreator)
  {
    _accessValidator = accessValidator;
    _validator = validator;
    _mapper = mapper;
    _repository = repository;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<bool>> ExecuteAsync(JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request)
  {
    if (!await _accessValidator.IsAdminAsync())
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
    }

    if (!_validator.ValidateCustom(request, out List<string> errors))
    {
      return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.BadRequest, errors);
    }

    OperationResultResponse<bool> response = new();

    response.Body = await _repository.EditAsync(await _mapper.Map(request));

    response.Status = response.Body
      ? OperationResultStatusType.FullSuccess
      : OperationResultStatusType.Failed;

    return response;
  }
}

