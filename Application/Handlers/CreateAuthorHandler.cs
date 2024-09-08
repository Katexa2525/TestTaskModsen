using Application.Commands;
using Application.Interfaces.Repository;
using Application.Mapping;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using MediatR;

namespace Application.Handlers
{
  internal sealed class CreateAuthorHandler: IRequestHandler<CreateAuthorCommand, AuthorDTO>
  {
    private readonly IRepositoryManager _repository;

    public CreateAuthorHandler(IRepositoryManager repository) => _repository = repository;

    public async Task<AuthorDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
      Author authorEntity = AuthorMapping.ToAuthor(request.Author);
      _repository.Author.CreateAuthor(authorEntity);
      await _repository.SaveAsync();
      AuthorDTO authorToReturn = AuthorMapping.ToAuthorResponse(authorEntity);
      return authorToReturn;
    }
  }
}
