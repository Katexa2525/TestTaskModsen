using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Mapping;
using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Domain.Entities.Validation;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Services
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
        UserBook userBook = UserBookMapping.ToUserBook(createUserBook);
        var validationResult = validator.Validate(userBook);
        if (validationResult.IsValid)
        {
          _repository.UserBook.PostBookToUserAsync(userBook);
          await _repository.SaveAsync();
          UserBookDTO userBookDTO = UserBookMapping.ToUserBookResponse(userBook);
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

    public async Task DeleteUserBookByIdAsync(Guid IdUserBook, bool trackChanges)
    {
        var userBookById = await _repository.UserBook.GetUserBookByUBId(IdUserBook, trackChanges);
        if (userBookById is null)
          throw new UserBookNotFoundException(IdUserBook);
        _repository.UserBook.DeleteUserBook(userBookById);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<UserBookDTO>> GetAllUserBooksAsync(bool trackChanges)
    {
        var userBooks = await _repository.UserBook.GetAllUserBooksAsync(trackChanges: false);
        IEnumerable<UserBookDTO> userBookToReturn = userBooks.Select(userBooks => userBooks.ToUserBookResponse());
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

        return userBook.ToUserBookResponse();
      }
      return null;
    }
  }
}
