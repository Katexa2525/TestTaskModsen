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
    Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
    Task<IEnumerable<Book>> GetBookByAuthorAsync(Guid authorId, bool trackChanges);
    Task<Book> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges);
    void DeleteBook(Book book);
    void CreateBook(Guid authorId, Book book);
  }
}
