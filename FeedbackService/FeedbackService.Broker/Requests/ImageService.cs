using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Broker.Requests.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models.Image;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.FeedbackService.Broker.Requests
{
  public class ImageService : IImageService
  {
    private readonly ILogger<ImageService> _logger;
    private readonly IRequestClient<ICreateImagesRequest> _rcCreateImages;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(
      ILogger<ImageService> logger,
      IRequestClient<ICreateImagesRequest> rcCreateImages,
      IHttpContextAccessor httpContextAccessor)
    {
      _logger = logger;
      _rcCreateImages = rcCreateImages;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Guid>> CreateImagesAsync(List<ImageContent> projectImages, List<string> errors = null)
    {
      return projectImages is null || !projectImages.Any()
        ? null
        : (await RequestHandler
          .ProcessRequest<ICreateImagesRequest, ICreateImagesResponse>(
            _rcCreateImages,
            ICreateImagesRequest.CreateObj(
              images: projectImages.Select(x => new CreateImageData(x.Name, x.Content, x.Extension)).ToList(),
              //TODO: change image source
              imageSource: ImageSource.Project,
              createdBy: _httpContextAccessor.HttpContext.GetUserId()),
            errors,
            _logger)).ImagesIds;
    }
  }
}
