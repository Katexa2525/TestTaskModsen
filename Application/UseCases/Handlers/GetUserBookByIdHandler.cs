using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Domain.Entities.Exceptions;
using Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Handlers
{
    internal sealed class GetUserBookByIdHandler : IRequestHandler<GetUserBookByIdQuery, UserBookDTO>
    {
        private readonly IRepositoryManager _repository;
        private User? _user;
        private readonly UserManager<User> _userManager;
        public GetUserBookByIdHandler(IRepositoryManager repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task<UserBookDTO> Handle(GetUserBookByIdQuery request, CancellationToken cancellationToken)
        {
            _user = await _userManager.FindByNameAsync(request.userName);
            if (_user != null)
            {
                var userBook = await _repository.UserBook.GetUserBookByIdAsync(request.bookId, _user.Id, trackChanges: false);
                if (userBook is null)
                    throw new UserBookNotFoundException(request.bookId);

                return userBook.ToUserBookResponse();
            }
            return null;
        }
    }
}
