using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
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
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      Book bookEntity = new Book 
      {
        Id = Guid.NewGuid(),
        Name = createBook.Name,
        ISBN = createBook.ISBN,
        Jenre = createBook.Jenre,
        ReturnTime = createBook.ReturnTime,
        TakeTime = createBook.TakeTime
      };
      //var bookEntity = _maper.Map<Book>(createBook);
      _repository.Book.CreateBook(authorId, bookEntity);
      await _repository.SaveAsync();
      //var bookToReturn = _maper.Map<BookDTO>(bookEntity);
      BookDTO bookToReturn = new BookDTO 
      {
        Id = bookEntity.Id,
        Name = bookEntity.Name,
        ISBN = bookEntity.ISBN,
        Jenre = bookEntity.Jenre,
        ReturnTime = bookEntity.ReturnTime,
        TakeTime = bookEntity.TakeTime,
      };
      return bookToReturn;
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
      IEnumerable<BookDTO> booksDTO = booksWithMetaData.Select(booksWithMetaData => new BookDTO
      {
        Id = booksWithMetaData.Id,
        Name = booksWithMetaData.Name,
        Jenre = booksWithMetaData.Jenre,
        ISBN = booksWithMetaData.ISBN,
        ReturnTime = booksWithMetaData.ReturnTime,
        TakeTime = booksWithMetaData.TakeTime,
      });
      //var booksDTO = _maper.Map<IEnumerable<BookDTO>>(booksWithMetaData);
      return (books: booksDTO, metaData: booksWithMetaData.MetaData);
    }

    public async Task<IEnumerable<BookDTO>> GetBookByAuthorAsync(Guid authorId, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(authorId);
      var booksFromDB = await _repository.Book.GetBookByAuthorAsync(authorId, trackChanges: false);
      //var booksDto = _maper.Map<IEnumerable<BookDTO>>(booksFromDB);
      IEnumerable<BookDTO> booksDto = booksFromDB.Select(booksFromDB => new BookDTO
      {
        Id = booksFromDB.Id,
        Name = booksFromDB.Name,
        Jenre = booksFromDB.Jenre,
        ISBN = booksFromDB.ISBN,
        ReturnTime = booksFromDB.ReturnTime,
        TakeTime = booksFromDB.TakeTime,
      });
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
      
      BookDTO book = new BookDTO 
      {
         Id = bookDb.Id,
         ISBN = bookDb.ISBN,
         Jenre = bookDb.Jenre,
         Name = bookDb.Name,
         ReturnTime = bookDb.ReturnTime,
         TakeTime = bookDb.TakeTime,
      };
      //var book = _maper.Map<BookDTO>(bookDb);
      return book;
    }

    public async Task UpdateBookAsync(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(authorId, authTrackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      var bookEntity = await _repository.Book.GetBookByIdAsync(authorId, id, bookTrackChanges);
      if (bookEntity is null)
        throw new BookNotFoundException(id);

      bookEntity.Name = bookUpdate.Name;
      bookEntity.ISBN = bookUpdate.ISBN;
      bookEntity.Jenre = bookUpdate.Jenre;
      bookEntity.ReturnTime = bookUpdate.ReturnTime;
      bookEntity.TakeTime = bookUpdate.TakeTime;

      //_maper.Map(bookUpdate, bookEntity);
      await _repository.SaveAsync();
    }
  }
}
