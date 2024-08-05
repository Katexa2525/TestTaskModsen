using Contracts.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModsenTestTask.Controllers
{
  [Route("api/books")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly IServiceManager _service;

    public BooksController(IServiceManager service)
    {
      _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public ActionResult GetAllBooks()
    {
      var book = _service.BookService.GetAllBooks(trackChanges: false);
      return Ok(book);
    }

    [HttpGet]
    public ActionResult GetBooksByAuthor(Guid authorId)
    {
      var book = _service.BookService.GetBookByAuthor(authorId, trackChanges: false);
      return Ok(book);
    }

    [HttpGet("{id:guid}")]
    public ActionResult GetBookById(Guid authorId, Guid id)
    {
      var book = _service.BookService.GetBookById(authorId, id, trackChanges: false);
      return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBook(Guid authorId, Guid id)
    {
      _service.BookService.DeleteBook(authorId, id, trackChanges: false);
      return NoContent();
    }

    [HttpPost]
    public IActionResult CreateBook(Guid authorId, [FromBody]CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateBookDTO object is null");
      var bookToReturn = _service.BookService.CreateBook(authorId, book, trackChanges: false);
      return CreatedAtRoute("GetBook", new
      {
        authorId,
        id = bookToReturn.Id
      },
      bookToReturn);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateBook(Guid authorId, Guid id, [FromBody] CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateUpdateBookDTO object is null");
      _service.BookService.UpdateBook(authorId, id, book, authTrackChanges: false, bookTrackChanges: true);
      return NoContent();
    }

  }
}
