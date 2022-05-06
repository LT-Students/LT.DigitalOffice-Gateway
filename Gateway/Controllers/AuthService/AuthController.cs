using System.Threading.Tasks;
using LT.DigitalOffice.AuthService.Models.Dto.Requests;
using LT.DigitalOffice.AuthService.Models.Dto.Responses;
using LT.DigitalOffice.Gateway.Clients.AuthServiceClients.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.Gateway.Controllers.AuthService
{
  [Route("[controller]")]
  [ApiController]
  public class AuthController
  {
    private readonly IAuthControllerClient _client;

    public AuthController(IAuthControllerClient client)
    {
      _client = client;
    }

    [HttpPost("login")]
    public async Task<LoginResult> LoginUser([FromBody] LoginRequest userCredentials)
    {
      return await _client.LoginUser(userCredentials);
    }

    [HttpPost("refresh")]
    public LoginResult RefreshToken([FromBody] RefreshRequest refreshToken)
    {
      return _client.RefreshToken(refreshToken);
    }
  }
}
