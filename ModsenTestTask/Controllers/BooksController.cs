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
    public async Task<ActionResult> GetAllBooks()
    {
      var book = await _service.BookService.GetAllBooksAsync(trackChanges: false);
      return Ok(book);
    }

    [HttpGet]
    public async Task<ActionResult> GetBooksByAuthor(Guid authorId)
    {
      var book = await _service.BookService.GetBookByAuthorAsync(authorId, trackChanges: false);
      return Ok(book);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetBookById(Guid authorId, Guid id)
    {
      var book = await _service.BookService.GetBookByIdAsync(authorId, id, trackChanges: false);
      return Ok(book);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook(Guid authorId, Guid id)
    {
      await _service.BookService.DeleteBookAsync(authorId, id, trackChanges: false);
      return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Guid authorId, [FromBody]CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateBookDTO object is null");
      var bookToReturn = await _service.BookService.CreateBookAsync(authorId, book, trackChanges: false);
      return CreatedAtRoute("GetBook", new
      {
        authorId,
        id = bookToReturn.Id
      },
      bookToReturn);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateBook(Guid authorId, Guid id, [FromBody] CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateUpdateBookDTO object is null");
      await _service.BookService.UpdateBookAsync(authorId, id, book, authTrackChanges: false, bookTrackChanges: true);
      return NoContent();
    }

  }
}
