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
  public class UserBookRepository: RepositoryBase<UserBook>, IUserBookRepository
  {
    public UserBookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void DeleteUserBook(UserBook userBook)
    {
      Delete(userBook);
    }

    public async Task<IEnumerable<UserBook>> GetAllUserBooksAsync(bool trackChanges) => await FindAll(trackChanges).ToListAsync();

    public async Task<UserBook> GetUserBookByIdAsync(Guid bookId, Guid userId, bool trackChanges)=>
       await FindByCondition(c => c.IdBook.Equals(bookId) && c.IdUser.Equals(userId), trackChanges).SingleOrDefaultAsync();

    public void PostBookToUserAsync(UserBook userBook)
    {
      Create(userBook);
    }
  }
}
