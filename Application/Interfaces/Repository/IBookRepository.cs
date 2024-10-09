using Domain.Entities.Models;
using Application.RequestFeatures;

namespace Application.Interfaces.Repository
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
