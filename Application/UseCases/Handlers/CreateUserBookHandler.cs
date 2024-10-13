using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Application.Exceptions;
using Domain.Entities.Models;
using Domain.Entities.Validation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    internal sealed class CreateUserBookHandler : IRequestHandler<CreateUserBookCommand, UserBookDTO>
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
            _user = await _userManager.FindByNameAsync(request.createUserBook.UserName);
            if (_user != null)
            {
                request.createUserBook.UserName = _user.Id;
                //var validator = new UserBookValidation();
                UserBook userBook = request.createUserBook.ToUserBook();
                //var validationResult = validator.Validate(userBook);
                //if (validationResult.IsValid)
                //{
                    _repository.UserBook.PostBookToUserAsync(userBook);
                    await _repository.SaveAsync();
                    UserBookDTO userBookDTO = userBook.ToUserBookResponse();
                    return userBookDTO;
                //}
            }
            return new UserBookDTO();
        }
    }
}
