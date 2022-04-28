using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.AuthService.Models.Dto.Requests;
using LT.DigitalOffice.AuthService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers.AdminService
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
      return await command.Execute(userCredentials);
    }

    [HttpPost("refresh")]
    public LoginResult RefreshToken([FromBody] RefreshRequest refreshToken)
    {
      return command.Execute(refreshToken);
    }
  }
}
