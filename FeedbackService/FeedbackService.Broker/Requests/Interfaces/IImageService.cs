using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Broker.Requests.Interfaces
{
  [AutoInject]
  public interface IImageService
  {
    Task<List<Guid>> CreateImagesAsync(List<ImageContent> projectImages, List<string> errors = null);
  }
}
