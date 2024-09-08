using Domain.Entities.DTO;
using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
  public static class UserBookMapping
  {
    public static UserBookDTO ToUserBookResponse(this UserBook userBook)
    {
      return new UserBookDTO
      {
        Id = userBook.Id,
        IdBook = userBook.IdBook,
        IdUser = userBook.IdUser,
      };
    }

    public static UserBook ToUserBook(this CreateUserBookDTO request)
    {
      return new UserBook
      {
        IdBook = request.IdBook,
        IdUser = request.UserName,
      };
    }
  }
}
