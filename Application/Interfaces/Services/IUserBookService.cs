using Domain.Entities.DTO;

namespace Application.Interfaces.Services
{
  public interface IUserBookService
  {
    Task<UserBookDTO> CreateUserBookAsync(CreateUserBookDTO createUserBook, bool trackChanges);
    Task<IEnumerable<UserBookDTO>> GetAllUserBooksAsync(bool trackChanges);
    Task<UserBookDTO> GetUserBookAsync(Guid bookId, string userId, bool trackChanges);
    Task DeleteUserBookAsync(Guid bookId, string userId, bool trackChanges);
    Task DeleteUserBookByIdAsync(Guid IdUserBook, bool trackChanges);
  }
}
