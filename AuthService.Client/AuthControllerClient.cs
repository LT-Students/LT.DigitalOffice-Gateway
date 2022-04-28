using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Data.Interfaces;

namespace LT.DigitalOffice.AdminService.Client
{
  public class AuthControllerClient : IAuthControllerClient
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.BaseAddress = new Uri("http://localhost:5158/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(BaseFindFilter filter)
    {
      FindResultResponse<ServiceConfigurationInfo> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:9838/admin/find?takecount={filter.TakeCount}&skipcount={filter.SkipCount}"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<FindResultResponse<ServiceConfigurationInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<bool>> EditAsync(List<Guid> servicesIds)
    {
      OperationResultResponse<bool> result = new();
      string token = _httpContextAccessor.HttpContext.Request.Headers["token"];

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:9838/admin/edit"))
      {
        request.Headers.Add("token", token);

        request.Content = JsonContent.Create(servicesIds);

        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());

        result.Status = (OperationResultStatusType)response.StatusCode;
      }

      return result;
    }
  }
}
