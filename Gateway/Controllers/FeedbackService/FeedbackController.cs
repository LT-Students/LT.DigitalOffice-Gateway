using System;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.Gateway.Controllers.FeedbackService
{
  [ApiController]
  [Route("[controller]")]
  public class FeedbackController : ControllerBase
  {
    private readonly IFeedbackControllerClient _client;

    public FeedbackController(IFeedbackControllerClient client)
    {
      _client = client;
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromBody] CreateFeedbackRequest request)
    {
      return await _client.CreateAsync(request);
    }
  }
}
