using Application.Commands;
using Application.Interfaces.Services;
using Application.Quaries;
using Domain.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
  [Route("api/userBooks")]
  [ApiController]
  public class UserBookController : ControllerBase
  {
    private readonly ISender _sender;
    public UserBookController(ISender sender) => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CrateUserBook([FromBody] CreateUserBookDTO userBook)
    {
      if (userBook is null)
        return BadRequest("UserBookDTO object is null");
      var userBookToReturn = await _sender.Send(new CreateUserBookCommand(userBook, trackChanges: false));
      if (userBookToReturn is null)
        return BadRequest("bookToReturn object is null");
      return Ok(userBookToReturn);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBooks()
    {
      var userBooks = await _sender.Send(new GetUserBooksQuery(trackChanges: false));
      return Ok(userBooks);
    }

    [HttpGet("{bookId:Guid}/{userName}", Name = "GetUserBookById")]
    public async Task<IActionResult> GetUserBookById(Guid bookId, string userName)
    {
      var userBook = await _sender.Send(new GetUserBookByIdQuery(bookId, userName, trackChanges: false));
      return Ok(userBook);
    }

    [HttpDelete("{bookId:Guid}/{userName}")]
    public async Task<IActionResult> DeleteUserBook(Guid bookId, string userName)
    {
      await _sender.Send(new DeleteUserBookCommand(bookId, userName, trackChanges: false));
      return NoContent();
    }

    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> DeleteUserBookById(Guid Id)
    {
      await _sender.Send(new DeleteUserBookByIdCommand(Id, trackChanges: false));
      return NoContent();
    }
  }
}
