using FluentValidation.Results;
using LT.DigitalOffice.FeedbackService.Broker.Requests.Interfaces;
using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback
{
  public class CreateFeedbackCommand : ICreateFeedbackCommand
  {
    private readonly IFeedbackRepository _repository;
    private readonly ICreateFeedbackValidator _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbFeedbackMapper _mapper;
    private readonly IResponseCreator _responseCreator;
    private readonly IImageService _imageService;
    private readonly ILogger<CreateFeedbackCommand> _logger;


    public CreateFeedbackCommand(
      IFeedbackRepository repository,
      ICreateFeedbackValidator validator,
      IHttpContextAccessor httpContextAccessor,
      IDbFeedbackMapper mapper,
      IResponseCreator responseCreator,
      IImageService imageService,
      ILogger<CreateFeedbackCommand> logger)
    {
      _repository = repository;
      _validator = validator;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _responseCreator = responseCreator;
      _imageService = imageService;
      _logger = logger;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateFeedbackRequest request)
    {
      //TODO: Fix
      var s = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
      _logger.LogInformation($"Remote IP is {s}");


      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();
      List<Guid> imageIds = await _imageService.CreateImagesAsync(request.FeedbackImages, response.Errors);
      DbFeedback dbFeedback = _mapper.Map(request, imageIds);

      response.Body = await _repository.CreateAsync(dbFeedback);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response.Body is null
        ? _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest)
        : response;
    }
  }
}
