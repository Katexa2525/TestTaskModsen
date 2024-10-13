using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
  public sealed class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, Unit>
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
      request.UpdateAuthor.Adapt(authorEntity);
      await _repository.SaveAsync();
      return Unit.Value;
    }
  }
}
