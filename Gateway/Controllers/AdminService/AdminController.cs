using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Gateway.Clients.AdminServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.Gateway.Controllers.AdminService
{
  [ApiController]
  [Route("{controller}")]
  public class AdminController : ControllerBase
  {
    private readonly IAdminControllerClient _client;

    public AdminController(IAdminControllerClient client)
    {
      _client = client;
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync([FromQuery] BaseFindFilter filter)
    {
      return await _client.FindAsync(filter);
    }

    [HttpPut("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync([FromBody] List<Guid> servicesIds)
    {
      return await _client.EditAsync(servicesIds);
    }

    [HttpPost("install")]
    public async Task<OperationResultResponse<bool>> InstallAppAsync([FromBody] InstallAppRequest request)
    {
      return await _client.InstallAsync(request);
    }
  }
}
