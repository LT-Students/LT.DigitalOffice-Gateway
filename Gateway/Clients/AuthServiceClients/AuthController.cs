using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Dto.Models;
using LT.DigitalOffice.AuthService.Models.Dto.Requests;
using LT.DigitalOffice.AuthService.Models.Dto.Responses;
using LT.DigitalOffice.Gateway.Clients.AuthServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Requests;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LT.DigitalOffice.Gateway.Clients.AuthServiceClients
{
  public class AuthController : IAuthControllerClient
  {
    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthController(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _client = new HttpClient();

      _client.BaseAddress = new Uri("http://localhost:5158/");
      _client.DefaultRequestHeaders.Accept.Clear();
      _client.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<LoginResult> LoginUser(LoginRequest userCredentials)
    {
      LoginResult result = new();

      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:9838/admin/find?takecount={userCredentials.LoginData}&skipcount={userCredentials.Password}"))
      {
        HttpResponseMessage response = await _client.SendAsync(request);

        result = JsonConvert.DeserializeObject<LoginResult>(
          await response.Content.ReadAsStringAsync());
      }

      return result;
    }

    public LoginResult RefreshToken(RefreshRequest refreshToken)
    {
      throw new NotImplementedException();
    }
  }
}
