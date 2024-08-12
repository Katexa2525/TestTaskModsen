using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Entities.Validation;
using Microsoft.AspNetCore.Identity;
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

    private readonly UserManager<User> _userManager;
    private User? _user;

    public UserBookService(IRepositoryManager repository, ILoggerManager logger, IMapper maper, UserManager<User> userManager)
    {
      _repository = repository;
      _logger = logger;
      _maper = maper;
      _userManager = userManager;
    }
    public async Task<UserBookDTO> CreateUserBookAsync(CreateUserBookDTO createUserBook, bool trackChanges)
    {
      _user = await _userManager.FindByNameAsync(createUserBook.UserName);
      if (_user != null)
      {
        createUserBook.UserName = _user.Id;
        var validator = new UserBookValidation();
        UserBook userBook = new UserBook
        {
          Id = Guid.NewGuid(),
          IdBook = createUserBook.IdBook,
          IdUser = createUserBook.UserName,
        };
        var validationResult = validator.Validate(userBook);
        if (validationResult.IsValid)
        {
          _repository.UserBook.PostBookToUserAsync(userBook);
          await _repository.SaveAsync();
          UserBookDTO userBookDTO = new UserBookDTO
          {
            Id = userBook.Id,
            IdBook = userBook.IdBook,
            UserName = _user.UserName,
            IdUser = userBook.IdUser,
          };
          return userBookDTO;
        }
      }
      return null;
    }

    public async Task DeleteUserBookAsync(Guid bookId, string userName, bool trackChanges)
    {
      _user = await _userManager.FindByNameAsync(userName);
      if (_user != null)
      {
        var userBookById = await _repository.UserBook.GetUserBookByIdAsync(bookId, _user.Id, trackChanges);
        if (userBookById is null)
          throw new UserBookNotFoundException(bookId);
        _repository.UserBook.DeleteUserBook(userBookById);
        await _repository.SaveAsync();
      }
    }

    public async Task<IEnumerable<UserBookDTO>> GetAllUserBooksAsync(bool trackChanges)
    {
        var userBooks = await _repository.UserBook.GetAllUserBooksAsync(trackChanges: false);
        IEnumerable<UserBookDTO> userBookToReturn = userBooks.Select(userBooks => new UserBookDTO
        {
          Id = userBooks.Id,
          IdBook = userBooks.IdBook,
          IdUser = userBooks.IdUser,
          UserName = _userManager.FindByIdAsync(userBooks.IdUser).Result is not null ? (string)_userManager.FindByIdAsync(userBooks.IdUser).Result.UserName : ""
        });
        return userBookToReturn;
    }

    public async Task<UserBookDTO> GetUserBookAsync(Guid bookId, string userName, bool trackChanges)
    {
      _user = await _userManager.FindByNameAsync(userName);
      if (_user != null)
      {
        var userBook = await _repository.UserBook.GetUserBookByIdAsync(bookId, _user.Id, trackChanges: false);
        if (userBook is null)
          throw new UserBookNotFoundException(bookId);

        var userBookDTO = new UserBookDTO
        {
          Id = userBook.Id,
          IdBook = userBook.IdBook,
          IdUser = userBook.IdUser,
        };
        return userBookDTO;
      }
      return null;
    }
  }
}
