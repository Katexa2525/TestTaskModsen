using Application.UseCases.Commands;
using Application.Interfaces.Services;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Domain.RequestFeatures;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace Presentation.Controllers
{
  [Route("api/books")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly ISender _sender;
    public BooksController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<ActionResult> GetAllBooks([FromQuery] BookParameters bookParameters)
    {
      var pagedResult = await _sender.Send(new GetBooksQuery(bookParameters, trackChanges: false));
      if (pagedResult.metaData is null)
        return BadRequest("pagedResultMeta object is null");
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
      return Ok(pagedResult.books);
    }

    [HttpGet("{authorId:Guid}")]
    public async Task<ActionResult> GetBooksByAuthor(Guid authorId)
    {
      var book = await _sender.Send(new GetBooksByAuthorQuery(authorId, trackChanges: false));
      return Ok(book);
    }

    [HttpGet("{authorId:Guid}/{id:Guid}", Name = "GetBookById")]
    public async Task<ActionResult> GetBookById(Guid authorId, Guid id)
    {
      var book = await _sender.Send(new GetBookByIdQuery(authorId, id, trackChanges: false));
      return Ok(book);
    }

    [HttpGet("{ISBN}", Name = "GetBookByISBN")]
    public async Task<ActionResult> GetBookByISBN(string ISBN)
    {
      var book = await _sender.Send(new GetBookByISBNQuery(ISBN, trackChanges: false));
      return Ok(book);
    }

    [HttpDelete("{authorId:Guid}/{id:Guid}")]
    public async Task<IActionResult> DeleteBook(Guid authorId, Guid id)
    {
      await _sender.Send(new DeleteBookCommand(authorId, id, trackChanges: false));
      return NoContent();
    }

    [HttpPost("{authorId:Guid}")]
    public async Task<IActionResult> CreateBook(Guid authorId, [FromForm]CreateUpdateBookDTO book)
    {
      if (book is null)
        return BadRequest("CreateBookDTO object is null");
      var bookToReturn = await _sender.Send(new CreateBookCommand(authorId, book, trackChanges: false));
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
      await _sender.Send(new UpdateBookCommand(authorId, id, book, authTrackChanges: false, bookTrackChanges: true));
      return NoContent();
    }

  }
}
