using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Mappers.Patch.Interfaces;
using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.ImageSupport.Helpers.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;

namespace LT.DigitalOffice.AdminService.Mappers.Patch;
public class PatchGraphicalUserInterfaceSettingMapper : IPatchGraphicalUserInterfaceSettingMapper
{
  private readonly IImageResizeHelper _resizeHelper;

  public PatchGraphicalUserInterfaceSettingMapper(
    IImageResizeHelper resizeHelper)
  {
    _resizeHelper = resizeHelper;
  }

  public async Task<JsonPatchDocument<DbGraphicalUserInterfaceSetting>> Map(
    JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request)
  {
    if (request is null)
    {
      return default;
    }

    JsonPatchDocument<DbGraphicalUserInterfaceSetting> dbPatch = new();

    foreach (Operation<EditGraphicalUserInterfaceSettingRequest> item in request.Operations)
    {
      if (item.path.EndsWith(nameof(EditGraphicalUserInterfaceSettingRequest.Logo)))
      {
        ImageConsist image = JsonConvert.DeserializeObject<ImageConsist>(item.value.ToString());

        (bool _, string resizedContent, string extension) = await _resizeHelper.ResizeAsync(
          image.Content, image.Extension);

        dbPatch.Operations.Add(new Operation<DbGraphicalUserInterfaceSetting>(
          item.op, nameof(DbGraphicalUserInterfaceSetting.LogoContent), item.from, resizedContent));

        dbPatch.Operations.Add(new Operation<DbGraphicalUserInterfaceSetting>(
          item.op, nameof(DbGraphicalUserInterfaceSetting.LogoExtension), item.from, extension));

        continue;
      }

      dbPatch.Operations.Add(new Operation<DbGraphicalUserInterfaceSetting>(
        item.op, item.path, item.from, item.value));
    }

    return dbPatch;
  }
}

