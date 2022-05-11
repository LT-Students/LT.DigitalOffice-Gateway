using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.Gateway.Clients.AdminServiceClients.Interfaces
{
  [AutoInject]
  public interface IAdminControllerClient
  {
    Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(BaseFindFilter filter);
    Task<OperationResultResponse<bool>> EditAsync(List<Guid> servicesIds);
    Task<OperationResultResponse<bool>> InstallAsync(InstallAppRequest request);
  }
}
