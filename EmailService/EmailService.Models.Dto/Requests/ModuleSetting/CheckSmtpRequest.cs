using System.ComponentModel.DataAnnotations;
using LT.DigitalOffice.EmailService.Models.Dto.Models;

namespace LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting
{
  public class CheckSmtpRequest //record - ?
  {
    [Required]
    public SmtpInfo SmtpInfo { get; set; }
    [Required]
    public string AdminEmail { get; set; }
  }
}
