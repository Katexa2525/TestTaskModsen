using Domain.Entities.DTO;
using Application.RequestFeatures;

namespace Application.Interfaces.Services
{
  public interface IBookService
  {
    Task<(IEnumerable<BookDTO> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<IEnumerable<BookDTO>> GetBookByAuthorAsync(Guid authorId, bool trackChanges);
    Task<BookDTO> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges);
    Task<BookDTO> GetBookByISBNAsync(string ISBN, bool trackChanges);
    Task DeleteBookAsync(Guid authorId, Guid id, bool trackChanges);
    Task<BookDTO> CreateBookAsync(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges);
    Task UpdateBookAsync(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges);
  }
}
