using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Services;
using Application.UseCases.Commands;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
  public sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDTO>
  {
    private readonly IRepositoryManager _repository;
    public CreateBookHandler(IRepositoryManager repository)
    public sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDTO>
    {
      _repository = repository;
    }
    public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
      //var validator = new BookValidator();
      var author = await _repository.Author.GetAuthorByIdAsync(request.authorId, request.trackChanges);
      if (author is null)
        throw new AuthorNotFoundException(request.authorId);
      TypeAdapterConfig<CreateUpdateBookDTO, Book>
  .NewConfig()
  .Ignore(dest => dest.Image);

      Book bookEntity = request.Book.Adapt<Book>();

      bookEntity.Image = ImageService.LoadImage(request.Book.Image);
      //var validationResult = validator.Validate(bookEntity);
      //if (validationResult.IsValid)
      //{
      _repository.Book.CreateBook(request.authorId, bookEntity);
      await _repository.SaveAsync();
      BookDTO bookToReturn = bookEntity.Adapt<BookDTO>();
      return bookToReturn;
      //}
      //return new BookDTO();
    }
  }
}
