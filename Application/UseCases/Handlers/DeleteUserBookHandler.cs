using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    public sealed class DeleteUserBookHandler : IRequestHandler<DeleteUserBookCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private User? _user;
        public DeleteUserBookHandler(IRepositoryManager repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<Unit> Handle(DeleteUserBookCommand request, CancellationToken cancellationToken)
        {
            _user = await _userManager.FindByNameAsync(request.userName);
            var userBookById = await _repository.UserBook.GetUserBookByIdAsync(request.bookId, _user.Id, request.trackChanges);
            if (userBookById is null)
                throw new UserBookNotFoundException(request.bookId);
            _repository.UserBook.DeleteUserBook(userBookById);
            await _repository.SaveAsync();
            return Unit.Value;

        }
    }
}
