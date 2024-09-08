using Domain.Entities.DTO;
using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
  public static class UserMapping
  {
    public static User ToUser(this UserForRegistrationDTO user)
    {
      return new User
      {
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        UserName = user.UserName,
        PasswordHash = user.PasswordHash,
      };
    }
  }
}
