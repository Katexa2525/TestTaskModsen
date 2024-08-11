using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IAuthorRepository
  {
    Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorByIdAsync(Guid authorId, bool trackChanges);
    void DeleteAuthor(Author author);
    void CreateAuthor(Author author);
  }
}
