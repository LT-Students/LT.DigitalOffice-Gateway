using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;

namespace LT.DigitalOffice.FeedbackService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FeedbackController : ControllerBase
  {
    [HttpGet("find")]
    public async Task<FindResultResponse<FeedbackInfo>> FindAsync(
      [FromQuery] FindFeedbacksFilter filter,
      [FromServices] IFindFeedbacksCommand command)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromBody] CreateFeedbackRequest request,
      [FromServices] ICreateFeedbackCommand command)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
