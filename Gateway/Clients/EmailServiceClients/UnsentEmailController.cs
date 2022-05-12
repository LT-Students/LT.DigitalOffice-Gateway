using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.Gateway.Clients.EmailServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.EmailServiceClients
{
  public class UnsentEmailController : IUnsentEmailController
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnsentEmailController(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<FindResultResponse<UnsentEmailInfo>> FindAsync(BaseFindFilter filter)
    {
      FindResultResponse<UnsentEmailInfo> result = new();
      string token = _httpContextAccessor.HttpContext.Request.Headers["token"];

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:9826/UnsentEmail/find?takecount={filter.TakeCount}&skipcount={filter.SkipCount}"))
      {
        if (!string.IsNullOrEmpty(token))
        {
          request.Headers.Add("token", token);
        }

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException();
        }

        result = JsonConvert.DeserializeObject<FindResultResponse<UnsentEmailInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<bool>> ResendAsync(Guid unsentEmailId)
    {
      OperationResultResponse<bool> result = new();
      string token = _httpContextAccessor.HttpContext.Request.Headers["token"];

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:9826/UnsentEmail/resend?unsentEmailId={unsentEmailId}"))
      {
        if (!string.IsNullOrEmpty(token))
        {
          request.Headers.Add("token", token);
        }

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException();
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
