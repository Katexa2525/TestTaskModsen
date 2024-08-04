using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
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

    public BookDTO CreateBook(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      var bookEntity = _maper.Map<Book>(createBook);
      _repository.Book.CreateBook(authorId, bookEntity);
      _repository.Save();
      var bookToReturn = _maper.Map<BookDTO>(bookEntity);
      return bookToReturn;
    }

    public void DeleteBook(Guid authorId,Guid id, bool trackChanges)
    {
      var bookById = _repository.Book.GetBookById(authorId,id, trackChanges);
      if (bookById is null)
        throw new BookNotFoundException(id);
      _repository.Book.DeleteBook(bookById);
      _repository.Save();
    }

    public IEnumerable<BookDTO> GetAllBooks(bool trackChanges)
    {
      var books = _repository.Book.GetAllBooks(trackChanges: false);
      var booksDTO = _maper.Map<IEnumerable<BookDTO>>(books);
      return booksDTO;
    }

    public IEnumerable<BookDTO> GetBookByAuthor(Guid authorId, bool trackChanges)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(authorId);
      var booksFromDB = _repository.Book.GetBookByAuthor(authorId, trackChanges: false);
      var booksDto = _maper.Map<IEnumerable<BookDTO>>(booksFromDB);
      return booksDto;
    }

    public BookDTO GetBookById(Guid authorId, Guid id, bool trackChanges)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(authorId);
      var bookDb = _repository.Book.GetBookById(authorId, id, trackChanges: false);
      if (bookDb is null)
        throw new BookNotFoundException(id);
      var book = _maper.Map<BookDTO>(bookDb);
      return book;
    }

    public void UpdateBook(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges)
    {
      var author = _repository.Author.GetAuthorById(authorId, authTrackChanges);
      if (author is null)
        throw new AuthorNotFoundException(authorId);
      var bookEntity = _repository.Book.GetBookById(authorId, id, bookTrackChanges);
      if (bookEntity is null)
        throw new BookNotFoundException(id);
      _maper.Map(bookUpdate, bookEntity);
      _repository.Save();
    }
  }
}
