using Entities.Models;

namespace Contracts
{
  public interface IUserBookRepository
  {
    void PostBookToUserAsync(UserBook userBook);
    Task<IEnumerable<UserBook>> GetAllUserBooksAsync(bool trackChanges);
    Task<UserBook> GetUserBookByIdAsync(Guid bookId, string userId, bool trackChanges);
    void DeleteUserBook(UserBook userBook);
  }
}
