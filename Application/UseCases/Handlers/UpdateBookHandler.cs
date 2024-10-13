using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Services;
using Application.UseCases.Commands;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
  public sealed class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Unit>
  {
    private readonly IRepositoryManager _repository;
    public UpdateBookHandler(IRepositoryManager repository)
    {
      _repository = repository;
    }
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.authTrackChanges);
      if (author is null)
        throw new AuthorNotFoundException(request.authorId);
      var bookEntity = await _repository.Book.GetBookByIdAsync(request.authorId, request.id, request.bookTrackChanges);
      if (bookEntity is null)
        throw new BookNotFoundException(request.id);
      request.bookUpdate.Adapt(bookEntity);
      bookEntity.Image = ImageService.LoadImage(request.bookUpdate.Image);
      await _repository.SaveAsync();
      return Unit.Value;
    }
  }
}
