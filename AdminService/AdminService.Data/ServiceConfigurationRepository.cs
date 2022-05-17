using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data
{
  public class ServiceConfigurationRepository : IServiceConfigurationRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServiceConfigurationRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> InstallAppAsync(List<Guid> servicesIdsToDisable)
    {
      int countDisabledServices = default;

      IQueryable<DbServiceConfiguration> configurations = _provider.ServicesConfigurations.AsQueryable();

      foreach (var service in configurations)
      {
        service.ModifiedAtUtc = DateTime.UtcNow;

        if (servicesIdsToDisable.Contains(service.Id))
        {
          service.IsActive = false;
          countDisabledServices++;
        }
      }

      await _provider.SaveAsync();

      return countDisabledServices;
    }

    public async Task<(List<DbServiceConfiguration> dbServicesConfigurations, int totalCount)> FindAsync(BaseFindFilter filter)
    {
      if (filter is null)
      {
        return (null, default);
      }

      IQueryable<DbServiceConfiguration> dbServicesConfigurations = _provider.ServicesConfigurations;

      return (
        await dbServicesConfigurations.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await dbServicesConfigurations.CountAsync());
    }

    public async Task<List<Guid>> EditAsync(List<Guid> servicesIds)
    {
      List<Guid> changedServicesIds = new();

      foreach (Guid serviceId in servicesIds)
      {
        DbServiceConfiguration dbServiceConfiguration = await _provider.ServicesConfigurations
          .FirstOrDefaultAsync(x => x.Id == serviceId);

        if (dbServiceConfiguration is null)
        {
          continue;
        }

        dbServiceConfiguration.IsActive = !dbServiceConfiguration.IsActive;
        dbServiceConfiguration.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
        dbServiceConfiguration.ModifiedAtUtc = DateTime.UtcNow;

        changedServicesIds.Add(serviceId);
      }

      await _provider.SaveAsync();

      return changedServicesIds;
    }

    public async Task<bool> DoesAppInstalledAsync()
    {
      return (await _provider.ServicesConfigurations
        .FirstAsync()).ModifiedAtUtc != default;
    }
  }
}
