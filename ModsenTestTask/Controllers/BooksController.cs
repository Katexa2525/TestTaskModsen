using Contracts.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Text.Json;

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
    //[Authorize(Roles = "User")]
    public async Task<ActionResult> GetAllBooks([FromQuery] BookParameters bookParameters)
    {
      var pagedResult = await _service.BookService.GetAllBooksAsync(bookParameters, trackChanges: false);
      if (pagedResult.metaData is null)
        return BadRequest("pagedResultMeta object is null");
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
      return Ok(pagedResult.books);
    }

    [HttpGet("{authorId:Guid}")]
    public async Task<ActionResult> GetBooksByAuthor(Guid authorId)
    {
      var book = await _service.BookService.GetBookByAuthorAsync(authorId, trackChanges: false);
      return Ok(book);
    }

    [HttpGet("{authorId:Guid}/{id:Guid}", Name = "GetBookById")]
    public async Task<ActionResult> GetBookById(Guid authorId, Guid id)
    {
      var book = await _service.BookService.GetBookByIdAsync(authorId, id, trackChanges: false);
      return Ok(book);
    }

    [HttpGet("{ISBN}", Name = "GetBookByISBN")]
    public async Task<ActionResult> GetBookByISBN(string ISBN)
    {
      var book = await _service.BookService.GetBookByISBNAsync(ISBN, trackChanges: false);
      return Ok(book);
    }

    [HttpDelete("{authorId:Guid}/{id:Guid}")]
    public async Task<IActionResult> DeleteBook(Guid authorId, Guid id)
    {
      await _service.BookService.DeleteBookAsync(authorId, id, trackChanges: false);
      return NoContent();
    }

    [HttpPost("{authorId:Guid}")]
    public async Task<IActionResult> CreateBook(Guid authorId, [FromForm]CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateBookDTO object is null");
      var bookToReturn = await _service.BookService.CreateBookAsync(authorId, book, trackChanges: false);
      if(bookToReturn is null)
        return BadRequest("bookToReturn object is null");
      return CreatedAtRoute("GetBookById", new
      {
        authorId,
        id = bookToReturn.Id
      },
      bookToReturn);
    }

    [HttpPut("{authorId:Guid}/{id:Guid}")]
    public async Task<IActionResult> UpdateBook(Guid authorId, Guid id, [FromForm] CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateUpdateBookDTO object is null");
      await _service.BookService.UpdateBookAsync(authorId, id, book, authTrackChanges: false, bookTrackChanges: true);
      return NoContent();
    }

  }
}
