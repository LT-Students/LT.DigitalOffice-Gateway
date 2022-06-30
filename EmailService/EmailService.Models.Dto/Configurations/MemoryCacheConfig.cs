namespace LT.DigitalOffice.EmailService.Models.Dto.Configurations
{
  public record MemoryCacheConfig
  {
    public const string SectionName = "MemoryCache";
    public double CacheLiveInMinutes { get; set; }
  }
}
