using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbFeedbackImageMapper
  {
    DbFeedbackImage Map(Guid imageId, Guid feedbackId);
  }
}
