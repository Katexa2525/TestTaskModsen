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

    public IEnumerable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

    public IEnumerable<Book> GetBookByAuthor(Guid authorId, bool trackChanges) => FindByCondition(c => c.IdAuthor.Equals(authorId), trackChanges).OrderBy(c => c.Name);
  }
}
