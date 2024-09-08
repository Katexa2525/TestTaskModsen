using Domain.Entities.DTO;
using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
  public static class AuthorMapping
  {
    public static AuthorDTO ToAuthorResponse(this Author author)
    {
      return new AuthorDTO
      {
        Id = author.Id,
        Name = author.Name,
        Surname = author.Surname,
        BirthdayDate = author.BirthdayDate,
        Country = author.Country,
      };
    }

    public static Author ToAuthor(this CreateAuthorDTO request)
    {
      return new Author
      {
        Name = request.Name,
        Surname = request.Surname,
        BirthdayDate = request.BirthdayDate,
        Country = request.Country,
      };
    }

    public static Author ToAuthor(this UpdateAuthorDTO request, Author author)
    {
      author.Name = request.Name;
      author.Surname = request.Surname;
      author.BirthdayDate = request.BirthdayDate;
      author.Country = request.Country;
      return author;
    }
  }
}
