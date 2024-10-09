﻿using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using Domain.Entities.Validation;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
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
            var validator = new BookValidator();
            var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.trackChanges);
            if (author is null)
                throw new AuthorNotFoundException(request.authorId);
            Book bookEntity = request.Book.Adapt<Book>();

            bookEntity.Image = ImageService.LoadImage(request.Book.Image);
            var validationResult = validator.Validate(bookEntity);
            if (validationResult.IsValid)
            {
                _repository.Book.CreateBook(request.authorId, bookEntity);
                await _repository.SaveAsync();
                BookDTO bookToReturn = bookEntity.Adapt<BookDTO>();
                return bookToReturn;
            }
            return null;
        }
    }
}
