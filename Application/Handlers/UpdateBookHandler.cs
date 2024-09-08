using Application.Commands;
using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using Domain.Entities.Exceptions;
using MediatR;

namespace Application.Handlers
{
  internal sealed class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Unit>
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

      bookEntity = BookMapping.ToBook(request.bookUpdate, bookEntity);

      bookEntity.Image = ImageService.LoadImage(request.bookUpdate.Image);
      await _repository.SaveAsync();
      return Unit.Value;
    }
  }
}
