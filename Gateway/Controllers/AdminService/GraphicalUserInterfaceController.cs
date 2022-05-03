using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Client.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.Gateway.Controllers.AdminService
{
  [ApiController]
  [Route("{controller}")]
  public class GraphicalUserInterfaceController : ControllerBase
  {
    private readonly IGraphicalUserInterfaceControllerClient _client;

    public GraphicalUserInterfaceController(IGraphicalUserInterfaceControllerClient client)
    {
      _client = client;
    }

    [HttpGet("get")]
    public async Task<OperationResultResponse<GuiInfo>> GetAsync()
    {
      return await _client.GetAsync();
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromBody] JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request)
    {
      return await _client.EditAsync(request);
    }
  }
}
