using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web.Http;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Gateway.Clients.AdminServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.AdminServiceClients
{
  public class AdminControllerClient : IAdminControllerClient
  {
    private readonly HttpClient _client;

    public AdminControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _client = new HttpClient();

      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<FindResultResponse<ServiceConfigurationInfo>> FindAsync(BaseFindFilter filter)
    {
      FindResultResponse<ServiceConfigurationInfo> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://admin.dev.ltdo.xyz/admin/find?takecount={filter.TakeCount}&skipcount={filter.SkipCount}"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        result = JsonConvert.DeserializeObject<FindResultResponse<ServiceConfigurationInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<bool>> EditAsync(List<Guid> servicesIds)
    {
      OperationResultResponse<bool> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://admin.dev.ltdo.xyz/admin/edit"))
      {
        request.Content = JsonContent.Create(servicesIds);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<bool>> InstallAsync(InstallAppRequest installRequest)
    {
      OperationResultResponse<bool> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://admin.dev.ltdo.xyz/admin/install"))
      {
        request.Content = JsonContent.Create(installRequest);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
