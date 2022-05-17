using System;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.AdminService.Data.Interfaces;

[AutoInject]
public interface IGraphicalUserInterfaceSettingRepository
{
  Task<Guid?> CreateAsync(DbGraphicalUserInterfaceSetting request);

  Task<bool> EditAsync(JsonPatchDocument<DbGraphicalUserInterfaceSetting> patch);

  Task<DbGraphicalUserInterfaceSetting> GetAsync();
}
