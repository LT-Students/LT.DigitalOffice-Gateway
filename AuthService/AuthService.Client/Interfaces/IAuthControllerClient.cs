using System.Threading.Tasks;
using LT.DigitalOffice.AuthService.Models.Dto.Requests;
using LT.DigitalOffice.AuthService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.AuthService.Client.Interfaces
{
  [AutoInject]
  public interface IAuthControllerClient
  {
    Task<LoginResult> LoginUser(LoginRequest userCredentials);

    LoginResult RefreshToken(RefreshRequest refreshToken);
  }
}
