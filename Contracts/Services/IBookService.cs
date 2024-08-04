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
    IEnumerable<BookDTO> GetAllBooks(bool trackChanges);
    IEnumerable<BookDTO> GetBookByAuthor(Guid authorId, bool trackChanges);
    BookDTO GetBookById(Guid authorId, Guid id, bool trackChanges);
    void DeleteBook(Guid authorId, Guid id, bool trackChanges);
    BookDTO CreateBook(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges);
    void UpdateBook(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges);
  }
}
