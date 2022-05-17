using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AdminService.Data.Provider;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data;
public class GraphicalUserInterfaceSettingRepository : IGraphicalUserInterfaceSettingRepository
{
  private readonly IDataProvider _provider;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public GraphicalUserInterfaceSettingRepository(
    IDataProvider provider,
    IHttpContextAccessor httpContextAccessor)
  {
    _provider = provider;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<Guid?> CreateAsync(DbGraphicalUserInterfaceSetting request)
  {
    if (request is null)
    {
      return null;
    }

    _provider.GraphicalUserInterfaceSettings.Add(request);
    await _provider.SaveAsync();

    return request.Id;
  }

  public async Task<bool> EditAsync(JsonPatchDocument<DbGraphicalUserInterfaceSetting> patch)
  {
    DbGraphicalUserInterfaceSetting dbGuiSetting = 
      await _provider.GraphicalUserInterfaceSettings.FirstOrDefaultAsync();

    if (dbGuiSetting is null)
    {
      return false;
    }

    patch.ApplyTo(dbGuiSetting);
    dbGuiSetting.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
    dbGuiSetting.ModifiedAtUtc = DateTime.UtcNow;

    await _provider.SaveAsync();

    return true;
  }

  public async Task<DbGraphicalUserInterfaceSetting> GetAsync()
  {
    return await _provider
      .GraphicalUserInterfaceSettings.FirstOrDefaultAsync();
  }
}
