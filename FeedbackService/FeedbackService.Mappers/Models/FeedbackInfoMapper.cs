using LT.DigitalOffice.FeedbackService.Mappers.Models.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;

namespace LT.DigitalOffice.FeedbackService.Mappers.Models
{
  public class FeedbackInfoMapper : IFeedbackInfoMapper
  {
    public FeedbackInfo Map(DbFeedback dbFeedback)
    {
      if (dbFeedback is null)
      {
        return null;
      }

      return new FeedbackInfo
      {
        Id = dbFeedback.Id,
        Type = (FeedbackType)dbFeedback.FeedbackType,
        Content = dbFeedback.Content,
        Status = (FeedbackStatusType)dbFeedback.Status,
        SenderFullName = dbFeedback.SenderFullName,
        SenderId = dbFeedback.SenderId,
        CreatedAtUtc = dbFeedback.CreatedAtUtc,
        ChangedBy = dbFeedback.ChangedBy,
        ChangedAtUtc = dbFeedback.ChangedAtUtc
      };
    }
  }
}
