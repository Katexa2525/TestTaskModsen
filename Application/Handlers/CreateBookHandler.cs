using Application.Commands;
using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Handlers
{
  internal sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDTO>
  {
    private readonly IRepositoryManager _repository;
    public CreateBookHandler(IRepositoryManager repository)
    {
      _repository = repository;
    }
    public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.trackChanges);
      if (author is null)
        throw new AuthorNotFoundException(request.authorId);
      Book bookEntity = BookMapping.ToBook(request.Book);

      bookEntity.Image = ImageService.LoadImage(request.Book.Image);

      _repository.Book.CreateBook(request.authorId, bookEntity);
      await _repository.SaveAsync();
      BookDTO bookToReturn = BookMapping.ToBookResponse(bookEntity);
      return bookToReturn;
    }
  }
}
