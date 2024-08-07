using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class BookRepository: RepositoryBase<Book>, IBookRepository
  {
    public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateBook(Guid authorId, Book book)
    {
      book.IdAuthor = authorId;
      Create(book);
    }

    public void DeleteBook(Book book)
    {
      Delete(book);
    }

    public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
      var books = await FindAll(trackChanges).Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize).Take(bookParameters.PageSize).Search(bookParameters.SearchTerm).ToListAsync();
      return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
    }
    public async Task<IEnumerable<Book>> GetBookByAuthorAsync(Guid authorId, bool trackChanges) => await FindByCondition(c => c.IdAuthor.Equals(authorId), trackChanges).OrderBy(c => c.Name).ToListAsync();

    public async Task<Book> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges) => await FindByCondition(c => c.IdAuthor.Equals(authorId) && c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
  }
}
