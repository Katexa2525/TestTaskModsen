using Application.Interfaces.Repository;
using Application.Mapping;
using Application.Services;
using Application.UseCases.Commands;
using Application.Exceptions;
using Domain.Entities.Validation;
using MediatR;

namespace Application.UseCases.Handlers
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
            //var validator = new BookValidator();
            var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.authTrackChanges);
            if (author is null)
                throw new AuthorNotFoundException(request.authorId);
            var bookEntity = await _repository.Book.GetBookByIdAsync(request.authorId, request.id, request.bookTrackChanges);
            if (bookEntity is null)
                throw new BookNotFoundException(request.id);

            //var validationResult = validator.Validate(bookEntity);
            //if (validationResult.IsValid)
            //{
                bookEntity = request.bookUpdate.ToBook(bookEntity);

                bookEntity.Image = ImageService.LoadImage(request.bookUpdate.Image);
                await _repository.SaveAsync();
                return Unit.Value;
            //}
            //return Unit.Value;
        }
    }
}
