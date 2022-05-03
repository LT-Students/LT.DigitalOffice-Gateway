using System;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Client.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.Gateway.Controllers.EmailService
{
  [Route("[controller]")]
  [ApiController]
  public class UnsentEmailController : ControllerBase
  {
    private readonly IUnsentEmailController _client;

    public UnsentEmailController(IUnsentEmailController client)
    {
      _client = client;
    }

    [HttpDelete("resend")]
    public async Task<OperationResultResponse<bool>> ResendAsync(
      [FromQuery] Guid unsentEmailId)
    {
      return await _client.ResendAsync(unsentEmailId);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<UnsentEmailInfo>> FindAsync(
      [FromQuery] BaseFindFilter filter)
    {
      return await _client.FindAsync(filter);
    }
  }
}

