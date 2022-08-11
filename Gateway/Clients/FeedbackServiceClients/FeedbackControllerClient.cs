using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web.Http;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Responses;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients
{
  public class FeedbackControllerClient : IFeedbackControllerClient
  {
    private readonly HttpClient _client;

    public FeedbackControllerClient()
    {
      _client = new HttpClient();
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<OperationResultResponse<Guid?>> CreateAsync(CreateFeedbackRequest request)
    {
      OperationResultResponse<Guid?> result = new();

      using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, $"https://feedback.dev.ltdo.xyz/feedback/create"))
      {
        message.Content = JsonContent.Create(request);

        HttpResponseMessage response = await _client.SendAsync(message);

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
          throw new ForbiddenException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
          throw new InternalServerException(response.Content.ReadAsAsync<HttpError>().Result.Message);
        }

        result = JsonConvert.DeserializeObject<OperationResultResponse<Guid?>>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
