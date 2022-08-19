using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Requests
{
  public record CreateFeedbackRequest
  {
    public FeedbackType Type { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; }

    [Required]
    public List<ImageContent> FeedbackImages { get; set; }
    public UserData User { get; set; } = null;
  }
}
