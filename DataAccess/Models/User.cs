using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
  public class User
  {
    [Column("UserId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "User name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Email is 50 characters.")]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public ICollection<UserBook> UserBooks { get; set; }  
  }
}
