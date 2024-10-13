using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.Exceptions;
using Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    internal sealed class DeleteUserBookByIdHandler : IRequestHandler<DeleteUserBookByIdCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        public DeleteUserBookByIdHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(DeleteUserBookByIdCommand request, CancellationToken cancellationToken)
        {
            var userBookById = await _repository.UserBook.GetUserBookByUBId(request.IdUserBook, request.trackChanges);
            if (userBookById is null)
                throw new UserBookNotFoundException(request.IdUserBook);
            _repository.UserBook.DeleteUserBook(userBookById);
            await _repository.SaveAsync();
            return Unit.Value;
        }
    }
}
