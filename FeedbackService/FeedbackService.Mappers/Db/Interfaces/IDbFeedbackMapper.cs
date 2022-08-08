using System;
using System.Collections.Generic;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbFeedbackMapper
  {
    DbFeedback Map(CreateFeedbackRequest request, List<Guid> imageIds);
  }
}
