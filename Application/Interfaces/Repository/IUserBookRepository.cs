using Domain.Entities.Models;

namespace Application.Interfaces.Repository
{
    public interface IUserBookRepository
  {
    void PostBookToUserAsync(UserBook userBook);
    Task<IEnumerable<UserBook>> GetAllUserBooksAsync(bool trackChanges);
    Task<UserBook> GetUserBookByIdAsync(Guid bookId, string userId, bool trackChanges);
    Task<UserBook> GetUserBookByUBId(Guid Id, bool trackChanges);
    void DeleteUserBook(UserBook userBook);
  }
}
