using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.EmailService.Business.Commands.ModuleSetting.Interfaces
{
  [AutoInject]
  public interface ICheckSmtpCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(CheckSmtpRequest request);
  }
}
