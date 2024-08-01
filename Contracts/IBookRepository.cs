using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IBookRepository
  {
    IEnumerable<Book> GetAllBooks(bool trackChanges);
    IEnumerable<Book> GetBookByAuthor(Guid authorId, bool trackChanges);
    Book GetBookById(Guid authorId, Guid id, bool trackChanges);
  }
}
