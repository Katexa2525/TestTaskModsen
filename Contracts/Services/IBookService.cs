using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
  public interface IBookService
  {
    Task<IEnumerable<BookDTO>> GetAllBooksAsync(bool trackChanges);
    Task<IEnumerable<BookDTO>> GetBookByAuthorAsync(Guid authorId, bool trackChanges);
    Task<BookDTO> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges);
    Task DeleteBookAsync(Guid authorId, Guid id, bool trackChanges);
    Task<BookDTO> CreateBookAsync(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges);
    Task UpdateBookAsync(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges);
  }
}
