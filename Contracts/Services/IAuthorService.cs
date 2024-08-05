using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracts.Services
{
  public interface IAuthorService
  {
    Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync(bool trackChanges);
    Task<AuthorDTO> GetAuthorAsync(Guid id, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
    Task<AuthorDTO> CreateAuthorAsync(CreateAuthorDTO author);
    Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges);

  }
}
