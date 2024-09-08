using Application.Interfaces.Repository;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserBookRepository : RepositoryBase<UserBook>, IUserBookRepository
    {
        public UserBookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void DeleteUserBook(UserBook userBook)
        {
            Delete(userBook);
        }

        public async Task<IEnumerable<UserBook>> GetAllUserBooksAsync(bool trackChanges) => await FindAll(trackChanges).ToListAsync();

        public async Task<UserBook> GetUserBookByIdAsync(Guid bookId, string userId, bool trackChanges) =>
           await FindByCondition(c => c.IdBook.Equals(bookId) && c.IdUser.Equals(userId), trackChanges).SingleOrDefaultAsync();

        public async Task<UserBook> GetUserBookByUBId(Guid Id, bool trackChanges) => await FindByCondition(c => c.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();

        public void PostBookToUserAsync(UserBook userBook)
        {
            Create(userBook);
        }
    }
}
