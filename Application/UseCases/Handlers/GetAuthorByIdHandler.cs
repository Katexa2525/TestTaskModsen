using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using AutoMapper;
using Domain.Entities.DTO;
using Mapster;
using Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Handlers
{
    public sealed class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly IRepositoryManager _repository;
        public GetAuthorByIdHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<AuthorDTO> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _repository.Author.GetAuthorByIdAsync(request.Id, request.trackChanges);
            if (author is null)
                throw new AuthorNotFoundException(request.Id);
            return author.Adapt<AuthorDTO>();
        }
    }
}
