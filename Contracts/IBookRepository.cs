using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IBookRepository
  {
    Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<IEnumerable<Book>> GetBookByAuthorAsync(Guid authorId, bool trackChanges);
    Task<Book> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges);
    Task<Book> GetBookByISBNAsync(string ISBN, bool trackChanges);
    void DeleteBook(Book book);
    void CreateBook(Guid authorId, Book book);
  }
}
