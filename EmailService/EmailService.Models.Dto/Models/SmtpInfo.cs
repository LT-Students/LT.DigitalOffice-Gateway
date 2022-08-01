using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.EmailService.Models.Dto.Models
{
  public record SmtpInfo
  {
    [Required]
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
