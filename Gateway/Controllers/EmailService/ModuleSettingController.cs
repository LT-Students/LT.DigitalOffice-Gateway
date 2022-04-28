﻿using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers.EmailService
{
  [ApiController]
  [Route("{controller}")]
  public class ModuleSettingController : ControllerBase
  {
    private readonly IModuleSettingControllerClient _client;

    public ModuleSettingController(IModuleSettingControllerClient client)
    {
      _client = client;
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromQuery] Guid moduleSettingId,
      [FromBody] JsonPatchDocument<EditModuleSettingRequest> patch)
    {
      return await _client.EditAsync(moduleSettingId, patch);
    }
  }
}