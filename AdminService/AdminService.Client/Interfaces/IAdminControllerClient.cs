using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IAdminControllerClient
  {
    Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(BaseFindFilter filter);

    Task<OperationResultResponse<bool>> EditAsync(List<Guid> servicesIds);
  }
}
