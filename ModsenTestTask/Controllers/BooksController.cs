using AutoMapper;
using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.Design;

namespace ModsenTestTask.Controllers
{
  [Route("api/books")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public BooksController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
    {
      _logger = logger;
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetAllBooks()
    {
      var books = _repository.Book.GetAllBooks(trackChanges: false);
      var booksDTO = _mapper.Map<IEnumerable<BookDTO>>(books);
      return Ok(booksDTO);
    }

    [HttpGet]
    public ActionResult GetBooksByAuthor(Guid authorId)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges: false);
      if (author == null)
      {
        _logger.LogInfo($"Author with id: {authorId} doesn't exist in the database.");
        return NotFound();
      }
      var booksFromDB = _repository.Book.GetBookByAuthor(authorId, trackChanges: false);
      var booksDto = _mapper.Map<IEnumerable<BookDTO>>(booksFromDB);
      return Ok(booksDto);
    }

    [HttpGet("{id}")]
    public ActionResult GetBookById(Guid authorId, Guid id)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges: false);
      if (author == null)
      {
        _logger.LogInfo($"Author with id: {authorId} doesn't exist in the database.");
        return NotFound();
      }
      var bookDb = _repository.Book.GetBookById(authorId, id, trackChanges: false);
      if (bookDb == null)
      {
        _logger.LogInfo($"Book with id: {id} doesn't exist in the database.");
        return NotFound();
      }
      var book = _mapper.Map<BookDTO>(bookDb);
      return Ok(book);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(Guid authorId, Guid id)
    {
      var author = _repository.Author.GetAuthorById(authorId, trackChanges: false);
      if (author == null)
      {
        _logger.LogInfo($"Author with id: {authorId} doesn't exist in the database.");
        return NotFound();
      }
      var book = _repository.Book.GetBookById(authorId, id, trackChanges: false);
      if (book == null)
      {
        _logger.LogInfo($"Book with id: {id} doesn't exist in the database.");
        return NotFound();
      }
      _repository.Book.DeleteBook(book);
      _repository.Save();
      return NoContent();
    }

  }
}
