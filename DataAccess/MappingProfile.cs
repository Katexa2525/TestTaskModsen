using AutoMapper;
using Entities.DTO;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Entities
{
  public class MappingProfile: Profile
  {
    public MappingProfile() 
    {
      CreateMap<Author, AuthorDTO>();
      CreateMap<Book, BookDTO>();
      CreateMap<CreateAuthorDTO, Author>();
      CreateMap<UpdateAuthorDTO, Author>();
      CreateMap<CreateUpdateBookDTO, Book>();
    }
  }
}
