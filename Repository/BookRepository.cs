using Contracts;
using Entities;
using Entities.Models;
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

    public IEnumerable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

    public IEnumerable<Book> GetBookByAuthor(Guid authorId, bool trackChanges) => FindByCondition(c => c.IdAuthor.Equals(authorId), trackChanges).OrderBy(c => c.Name);

    public Book GetBookById(Guid authorId, Guid id, bool trackChanges) => FindByCondition(c => c.IdAuthor.Equals(authorId) && c.Id.Equals(id), trackChanges).SingleOrDefault();
  }
}
