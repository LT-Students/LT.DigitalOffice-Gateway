using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Client.Interfaces;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AdminService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace LT.DigitalOffice.AdminService.Client
{
  public class GraphicalUserInterfaceControllerClient : IGraphicalUserInterfaceControllerClient
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GraphicalUserInterfaceControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.BaseAddress = new Uri("http://localhost:5158/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<OperationResultResponse<bool>> EditAsync(JsonPatchDocument<EditGraphicalUserInterfaceSettingRequest> request)
    {
      OperationResultResponse<bool> result = new();

      using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, $"http://localhost:9838/GraphicalUserInterface/edit"))
      {
        requestMessage.Content = JsonContent.Create(request);


        HttpResponseMessage response = await _client.SendAsync(requestMessage);
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException();
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<bool>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<OperationResultResponse<GuiInfo>> GetAsync()
    {
      OperationResultResponse<GuiInfo> result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:9838/GraphicalUserInterface/get"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<OperationResultResponse<GuiInfo>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
