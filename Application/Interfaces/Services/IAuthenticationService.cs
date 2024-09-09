using Domain.Entities.DTO;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Services
{
  public interface IAuthenticationService
  {
    Task<IdentityResult> RegisterUser(UserForRegistrationDTO userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDTO userForAuth);
    Task<string> CreateToken();
  }
}
