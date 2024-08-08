using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    //public string? UserName { get; init; }
    //public string? Password { get; init; }
    //public string? Email { get; init; }
    //public string? PhoneNumber { get; init; }
    //public ICollection<string>? Roles { get; set; }

    //public string? RefreshToken { get; set; }
    //public DateTime RefreshTokenExpiryTime { get; set; }
  }
}
