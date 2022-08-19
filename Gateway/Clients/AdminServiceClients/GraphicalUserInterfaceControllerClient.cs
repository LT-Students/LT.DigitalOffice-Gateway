using System;
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
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.AdminServiceClients
{
  public class GraphicalUserInterfaceControllerClient : IGraphicalUserInterfaceControllerClient
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GraphicalUserInterfaceControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<OperationResultResponse<bool>> EditAsync(JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request)
    {
      OperationResultResponse<bool> result = new();

      using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, $"https://admin.dev.ltdo.xyz/GraphicalUserInterface/edit"))
      {
        requestMessage.Content = JsonContent.Create(request);


        HttpResponseMessage response = await _client.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
          throw new NotFoundException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<GuiInfo>> GetAsync()
    {
      OperationResultResponse<GuiInfo> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://admin.dev.ltdo.xyz/GraphicalUserInterface/get"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<OperationResultResponse<GuiInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
