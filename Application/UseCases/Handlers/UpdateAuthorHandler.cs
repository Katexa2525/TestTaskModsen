﻿using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Commands;
using Domain.Entities.Exceptions;
using Domain.Entities.Validation;
using MediatR;

namespace Application.UseCases.Handlers
{
    internal sealed class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        public UpdateAuthorHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var validator = new AuthorValidator();
            var authorEntity = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.trackChanges);
            if (authorEntity is null)
                throw new AuthorNotFoundException(request.authorId);
            var validationResult = validator.Validate(authorEntity);
            if (validationResult.IsValid)
            {
                authorEntity = request.UpdateAuthor.ToAuthor(authorEntity);
                await _repository.SaveAsync();
                return Unit.Value;
            }
            return Unit.Value;
        }
    }
}
