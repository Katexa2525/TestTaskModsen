﻿using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Quaries;
using Domain.Entities.DTO;
using MediatR;

namespace Application.Handlers
{
  internal sealed class GetUserBooksHandler : IRequestHandler<GetUserBooksQuery, IEnumerable<UserBookDTO>>
  {
    private readonly IRepositoryManager _repository;
    public GetUserBooksHandler(IRepositoryManager repository) => _repository = repository;
    public async Task<IEnumerable<UserBookDTO>> Handle(GetUserBooksQuery request, CancellationToken cancellationToken)
    {
      var userBooks = await _repository.UserBook.GetAllUserBooksAsync(trackChanges: false);
      IEnumerable<UserBookDTO> userBookToReturn = userBooks.Select(userBooks => userBooks.ToUserBookResponse());
      return userBookToReturn;
    }
  }
}
