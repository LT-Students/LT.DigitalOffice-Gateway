using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Dto.Requests.ModuleSetting;
using LT.DigitalOffice.Gateway.Clients.EmailServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.EmailServiceClients
{
  public class ModuleSettingControllerClient : IModuleSettingControllerClient
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ModuleSettingControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<OperationResultResponse<bool>> EditAsync(Guid moduleSettingId, JsonPatchDocument<EditModuleSettingRequest> patch)
    {
      OperationResultResponse<bool> result = new();
      string token = _httpContextAccessor.HttpContext.Request.Headers["token"];

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, $"http://localhost:9826/ModuleSetting/edit?moduleSettingId={moduleSettingId}"))
      {
        request.Headers.Add("token", token);

        request.Content = JsonContent.Create(patch);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException();
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());

        result.Status = (OperationResultStatusType)response.StatusCode;
      }

      return result;
    }
  }
}
