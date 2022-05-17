using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Business.Commands.ServiceConfiguration.Interfaces
{
  [AutoInject]
  public interface IFindServiceConfigurationCommand
  {
    Task<FindResultResponse<ServiceConfigurationInfo>> ExecuteAsync(BaseFindFilter filter);
  }
}
