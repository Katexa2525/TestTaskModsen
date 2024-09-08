using Application.Commands;
using Application.Interfaces.Repository;
using Application.Mapping;
using Domain.Entities.Exceptions;
using MediatR;

namespace Application.Handlers
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
      var authorEntity = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.trackChanges);
      if (authorEntity is null)
        throw new AuthorNotFoundException(request.authorId);
      authorEntity = AuthorMapping.ToAuthor(request.UpdateAuthor, authorEntity);
      await _repository.SaveAsync();
      return Unit.Value;
    }
  }
}
