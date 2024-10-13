using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using AutoMapper;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    internal sealed class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
    {
        private readonly IRepositoryManager _repository;

        public GetBookByIdHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<BookDTO> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, trackChanges: false);
            if (author == null)
                throw new AuthorNotFoundException(request.authorId);
            var book = await _repository.Book.GetBookByIdAsync(request.authorId, request.Id, request.trackChanges);
            if (book is null)
                throw new BookNotFoundException(request.Id);
            return book.Adapt<BookDTO>();
        }
    }
}
