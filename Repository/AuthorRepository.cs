using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class AuthorRepository: RepositoryBase<Author>, IAuthorRepository
  {
    public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateAuthor(Author author)
    {
      Create(author);
    }

    public void DeleteAuthor(Author author)
    {
      Delete(author);
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

    public async Task<Author> GetAuthorByIdAsync(Guid authorId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(authorId), trackChanges).SingleOrDefaultAsync();
  }
}
