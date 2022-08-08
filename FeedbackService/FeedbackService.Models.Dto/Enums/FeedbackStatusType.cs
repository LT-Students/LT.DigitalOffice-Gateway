using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Enums
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum FeedbackStatusType
  {
    New,
    InProgress,
    Done
  }
}
