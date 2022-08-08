using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
  }
}
