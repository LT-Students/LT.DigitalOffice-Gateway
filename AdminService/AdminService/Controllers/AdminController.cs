using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.AdminService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    [HttpGet("find")]
    public async Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(
      [FromServices] IFindServiceConfigurationCommand command,
      [FromQuery] BaseFindFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPut("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditServiceConfigurationCommand command,
      [FromBody] List<Guid> servicesIds)
    {
      return await command.ExecuteAsync(servicesIds);
    }

    [HttpPost("install")]
    public async Task<OperationResultResponse<bool>> InstallAppAsync(
      [FromServices] IInstallAppCommand command,
      [FromBody] InstallAppRequest request)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
