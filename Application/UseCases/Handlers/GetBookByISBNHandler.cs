using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    public sealed class GetBookByISBNHandler : IRequestHandler<GetBookByISBNQuery, BookDTO>
    {
        private readonly IRepositoryManager _repository;

        public GetBookByISBNHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<BookDTO> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.Book.GetBookByISBNAsync(request.ISBN, request.trackChanges);
            if (book is null)
                throw new BookNotFoundByISBN(request.ISBN);
            return book.ToBookResponse();
        }
    }
}
