using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Client.Interfaces;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.DigitalOffice.EmailService.Client
{
  public class UnsentEmailController : IUnsentEmailController
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnsentEmailController(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.BaseAddress = new Uri("http://localhost:5158/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<FindResultResponse<UnsentEmailInfo>> FindAsync(BaseFindFilter filter)
    {
      FindResultResponse<UnsentEmailInfo> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:9826/UnsentEmail/find?takecount={filter.TakeCount}&skipcount={filter.SkipCount}"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<FindResultResponse<UnsentEmailInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<bool>> ResendAsync(Guid unsentEmailId)
    {
      OperationResultResponse<bool> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:9826/UnsentEmail/resend?unsentEmailId=unsentEmailId"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
