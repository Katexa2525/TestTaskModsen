using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Quaries;
using Domain.Entities.DTO;
using MediatR;

namespace Application.Handlers
{
  internal sealed class GetAuthorsHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<AuthorDTO>>
  {
    private readonly IRepositoryManager _repository;
    public GetAuthorsHandler(IRepositoryManager repository) => _repository = repository;
    public async Task<IEnumerable<AuthorDTO>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
      var authors = await _repository.Author.GetAllAuthorsAsync(request.TrackChanges);
      IEnumerable<AuthorDTO> authorToReturn = authors.Select(authors => authors.ToAuthorResponse());
      return authorToReturn;
    }
  }
}
