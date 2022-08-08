using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using System;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db
{
  public class DbFeedbackImageMapper : IDbFeedbackImageMapper
  {
    public DbFeedbackImage Map(Guid imageId, Guid feedbackId)
    {
      return new DbFeedbackImage
      {
        Id = Guid.NewGuid(),
        ImageId = imageId,
        FeedbackId = feedbackId
      };
    }
  }
}
