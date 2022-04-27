using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.AdminService.Mappers.Patch.Interfaces;

[AutoInject]
public interface IPatchGraphicalUserInterfaceSettingMapper
{
  Task<JsonPatchDocument<DbGraphicalUserInterfaceSetting>> Map(
    JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request);
}

