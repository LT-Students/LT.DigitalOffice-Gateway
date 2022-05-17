using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Requests;

namespace LT.DigitalOffice.AdminService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceConfigurationRepository
  {
    Task<int> InstallAppAsync(List<Guid> servicesIdsToDisable);

    Task<(List<DbServiceConfiguration> dbServicesConfigurations, int totalCount)> FindAsync(BaseFindFilter filter);

    Task<List<Guid>> EditAsync(List<Guid> servicesIds);

    Task<bool> DoesAppInstalledAsync();
  }
}
