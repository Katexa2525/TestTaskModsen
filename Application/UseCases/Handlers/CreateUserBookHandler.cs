using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Handlers
{
  public sealed class CreateUserBookHandler : IRequestHandler<CreateUserBookCommand, UserBookDTO>
  {
    private readonly IRepositoryManager _repository;
    private readonly UserManager<User> _userManager;
    private User? _user;

    public CreateUserBookHandler(IRepositoryManager repository, UserManager<User> userManager)
    {
      _repository = repository;
      _userManager = userManager;
    }
    public async Task<UserBookDTO> Handle(CreateUserBookCommand request, CancellationToken cancellationToken)
    {
      _user = await _userManager.FindByNameAsync(request.createUserBook.IdUser);
      if (_user != null)
      {
        request.createUserBook.IdUser = _user.Id;
        UserBook userBook = request.createUserBook.ToUserBook();
        _repository.UserBook.PostBookToUserAsync(userBook);
        await _repository.SaveAsync();
        UserBookDTO userBookDTO = userBook.Adapt<UserBookDTO>();
        return userBookDTO;
      }
      return new UserBookDTO();
    }
  }
}
