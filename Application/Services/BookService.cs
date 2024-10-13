using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities.Validation;
using Application.Exceptions;
using Application.RequestFeatures;
using Application.Mapping;

namespace Application.Services
{
  internal sealed class BookService: IBookService
  {
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _maper;

    public BookService(IRepositoryManager repository, ILoggerManager logger, IMapper maper) 
    {
      _repository = repository;
      _logger = logger;
      _maper = maper;
    }

    public async Task<BookDTO> CreateBookAsync(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges)
    {
      var validator = new BookValidator();
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      Book bookEntity = BookMapping.ToBook(createBook);

      bookEntity.Image = ImageService.LoadImage(createBook.Image);

      var validationResult = validator.Validate(bookEntity);
      if (validationResult.IsValid)
      {
        _repository.Book.CreateBook(authorId, bookEntity);
        await _repository.SaveAsync();
        BookDTO bookToReturn = BookMapping.ToBookResponse(bookEntity);
        return bookToReturn;
      }
      return null;
    }
  
    public async Task DeleteBookAsync(Guid authorId,Guid id, bool trackChanges)
    {
      var bookById = await _repository.Book.GetBookByIdAsync(authorId,id, trackChanges);
      if (bookById is null)
        throw new BookNotFoundException(id);
      _repository.Book.DeleteBook(bookById);
      await _repository.SaveAsync();
    }

    public async Task<(IEnumerable<BookDTO> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
      var booksWithMetaData = await _repository.Book.GetAllBooksAsync(bookParameters, trackChanges);
      IEnumerable<BookDTO> booksDTO = booksWithMetaData.Select(booksWithMetaData => booksWithMetaData.ToBookResponse());
      return (books: booksDTO, metaData: booksWithMetaData.MetaData);
    }

    public async Task<IEnumerable<BookDTO>> GetBookByAuthorAsync(Guid authorId, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(authorId);
      var booksFromDB = await _repository.Book.GetBookByAuthorAsync(authorId, trackChanges: false);
      IEnumerable<BookDTO> booksDto = booksFromDB.Select(booksFromDB => booksFromDB.ToBookResponse()).ToList();
      return booksDto;
    }

    public async Task<BookDTO> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(authorId);
      var bookDb = await _repository.Book.GetBookByIdAsync(authorId, id, trackChanges: false);
      if (bookDb is null)
        throw new BookNotFoundException(id);

      return bookDb.ToBookResponse();
    }

    public async Task<BookDTO> GetBookByISBNAsync(string ISBN, bool trackChanges)
    {
      var bookDb = await _repository.Book.GetBookByISBNAsync(ISBN, trackChanges: false);
      if (bookDb is null)
        throw new BookNotFoundByISBN(ISBN);

      return bookDb.ToBookResponse();
    }

    public async Task UpdateBookAsync(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges)
    {
      var validator = new BookValidator();
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, authTrackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      var bookEntity = await _repository.Book.GetBookByIdAsync(authorId, id, bookTrackChanges);
      if (bookEntity is null)
        throw new BookNotFoundException(id);
      var validationResult = validator.Validate(bookEntity);
      if (validationResult.IsValid)
      {
        bookEntity = BookMapping.ToBook(bookUpdate, bookEntity);

        bookEntity.Image = ImageService.LoadImage(bookUpdate.Image);
        await _repository.SaveAsync();
      }
    }
  }
}
