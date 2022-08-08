using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Models
{
  public record FeedbackInfo
  {
    public Guid Id { get; set; }
    public FeedbackType Type { get; set; }
    public string Content { get; set; }
    public FeedbackStatusType Status { get; set; }
    public string SenderFullName { get; set; }
    public Guid SenderId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ChangedBy { get; set; }
    public DateTime? ChangedAtUtc { get; set; }
  }
}
