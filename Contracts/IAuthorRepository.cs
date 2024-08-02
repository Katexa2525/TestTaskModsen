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
    IEnumerable<Author> GetAllAuthors(bool trackChanges);
    Author GetAuthorById(Guid authorId, bool trackChanges);
    void DeleteAuthor(Author author);
    void CreateAuthor(Author author);
  }
}
