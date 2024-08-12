using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validation
{
  public class UserBookValidation: AbstractValidator<UserBook>
  {
    public UserBookValidation() 
    {
      RuleFor(userBook => userBook.IdBook).NotNull();
      RuleFor(userBook => userBook.IdUser).NotNull();
    }
  }
}
