using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  internal sealed class UserBookService : IUserBookService
  {
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _maper;

    public UserBookService(IRepositoryManager repository, ILoggerManager logger, IMapper maper)
    {
      _repository = repository;
      _logger = logger;
      _maper = maper;
    }
    public async Task<UserBookDTO> CreateUserBookAsync(CreateUserBookDTO createUserBook, bool trackChanges)
    {
      UserBook userBook = new UserBook
      {
        Id = Guid.NewGuid(),
        IdBook = createUserBook.IdBook,
        IdUser = createUserBook.IdUser,
      };
      _repository.UserBook.PostBookToUserAsync(userBook);
      await _repository.SaveAsync();
      UserBookDTO userBookDTO = new UserBookDTO
      {
        //Id = userBook.Id,
        IdBook = userBook.IdBook,
        IdUser = userBook.IdUser,
      };
      return userBookDTO;
    }

    public async Task DeleteUserBookAsync(Guid bookId, string userId, bool trackChanges)
    {
      var userBookById = await _repository.UserBook.GetUserBookByIdAsync(bookId, userId, trackChanges);
      if (userBookById is null)
        throw new UserBookNotFoundException(bookId);
      _repository.UserBook.DeleteUserBook(userBookById);
      await _repository.SaveAsync();
    }

    public async Task<IEnumerable<UserBookDTO>> GetAllUserBooksAsync(bool trackChanges)
    {
      var userBooks = await _repository.UserBook.GetAllUserBooksAsync(trackChanges: false);
      //var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
      IEnumerable<UserBookDTO> userBookToReturn = userBooks.Select(userBooks => new UserBookDTO
      {
        Id = userBooks.Id,
        IdBook = userBooks.IdBook,
        IdUser = userBooks.IdUser,
      });
      return userBookToReturn;
    }

    public async Task<UserBookDTO> GetUserBookAsync(Guid bookId, string userId, bool trackChanges)
    {
      var userBook = await _repository.UserBook.GetUserBookByIdAsync(bookId, userId, trackChanges: false);
      if (userBook is null)
        throw new UserBookNotFoundException(bookId);

      var userBookDTO = new UserBookDTO
      {
        Id = userBook.Id,
        IdBook = userBook.IdBook,
        IdUser = userBook.IdUser,
      };//_mapper.Map<AuthorDTO>(author);
      return userBookDTO;
    }
  }
}
