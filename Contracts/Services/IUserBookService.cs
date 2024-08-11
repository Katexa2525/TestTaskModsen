using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
  public interface IUserBookService
  {
    Task<UserBookDTO> CreateUserBookAsync(CreateUserBookDTO createUserBook, bool trackChanges);
    Task<IEnumerable<UserBookDTO>> GetAllUserBooksAsync(bool trackChanges);
    Task<UserBookDTO> GetUserBookAsync(Guid bookId, string userId, bool trackChanges);
    Task DeleteUserBookAsync(Guid bookId, string userId, bool trackChanges);
  }
}
