using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Domain.Entities.Validation;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
    internal sealed class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, AuthorDTO>
    {
        private readonly IRepositoryManager _repository;

        public CreateAuthorHandler(IRepositoryManager repository) => _repository = repository;

        public async Task<AuthorDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var validator = new AuthorValidator();
            Author authorEntity = request.Author.Adapt<Author>();
            var validationResult = validator.Validate(authorEntity);
            if (validationResult.IsValid)
            {
                _repository.Author.CreateAuthor(authorEntity);
                await _repository.SaveAsync();
            AuthorDTO authorToReturn = authorEntity.Adapt<AuthorDTO>();
            return authorToReturn;
            }
            return null;
        }
    }
}
