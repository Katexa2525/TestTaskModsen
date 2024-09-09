﻿using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Quaries;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
  internal sealed class GetBooksByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, IEnumerable<BookDTO>>
  {
    private readonly IRepositoryManager _repository;

    public GetBooksByAuthorHandler(IRepositoryManager repository)
    {
      _repository = repository;
    }
    public async Task<IEnumerable<BookDTO>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(request.authorId);
      var booksFromDB = await _repository.Book.GetBookByAuthorAsync(request.authorId, trackChanges: false);
      IEnumerable<BookDTO> booksDto = booksFromDB.Select(booksFromDB => booksFromDB.ToBookResponse()).ToList();
      return booksDto;
    }
  }
}
