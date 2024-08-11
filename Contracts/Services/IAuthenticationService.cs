using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
  public interface IAuthenticationService
  {
    Task<IdentityResult> RegisterUser(UserForRegistrationDTO userForRegistration);
    Task<bool> ValidateUser(UserForAuthenticationDTO userForAuth);
    Task<string> CreateToken();
  }
}
