using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
    public sealed class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDTO>>
    {
        private readonly IRepositoryManager _repository;
        public GetAuthorsHandler(IRepositoryManager repository) => _repository = repository;
        public async Task<IEnumerable<AuthorDTO>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _repository.Author.GetAllAuthorsAsync(request.TrackChanges);
            IEnumerable<AuthorDTO> authorToReturn = authors.Select(authors => authors.Adapt<AuthorDTO>());
            return authorToReturn;
        }
    }
}
