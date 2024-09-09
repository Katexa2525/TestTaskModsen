using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    internal sealed class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        public DeleteBookHandler(IRepositoryManager repository) => _repository = repository;
        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookById = await _repository.Book.GetBookByIdAsync(request.authorId, request.id, request.trackChanges);
            if (bookById is null)
                throw new BookNotFoundException(request.id);
            _repository.Book.DeleteBook(bookById);
            await _repository.SaveAsync();
            return Unit.Value;
        }
    }
}
