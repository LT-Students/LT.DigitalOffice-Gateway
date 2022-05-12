using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LT.DigitalOffice.AuthService.Models.Dto.Requests;
using LT.DigitalOffice.AuthService.Models.Dto.Responses;
using LT.DigitalOffice.Gateway.Clients.AuthServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.AuthServiceClients
{
  public class AuthControllerClient : IAuthControllerClient
  {
    private readonly HttpClient _client;

    public AuthControllerClient(IHttpContextAccessor httpContextAccessor)
    {
      _client = new HttpClient();

      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<LoginResult> LoginUser(LoginRequest userCredentials)
    {
      LoginResult result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:9818/Auth/login"))
      {
        request.Content = JsonContent.Create(userCredentials);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException();
        }

        result = JsonConvert.DeserializeObject<LoginResult>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public async Task<LoginResult> RefreshTokenAsync(RefreshRequest refreshToken)
    {
      LoginResult result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:9818/Auth/refresh"))
      {
        request.Content = JsonContent.Create(refreshToken);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
          throw new BadRequestException();
        }

        result = JsonConvert.DeserializeObject<LoginResult>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }
  }
}
