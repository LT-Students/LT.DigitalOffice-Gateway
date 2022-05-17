using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces
{
  [AutoInject]
  public interface IInstallAppCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(InstallAppRequest request);
  }
}
