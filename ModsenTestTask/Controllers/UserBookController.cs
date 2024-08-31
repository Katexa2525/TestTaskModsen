using Application.Interfaces.Services;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
  [Route("api/userBooks")]
  [ApiController]
  public class UserBookController : ControllerBase
  {
    private readonly IServiceManager _service;

    public UserBookController(IServiceManager service)
    {
      _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CrateUserBook([FromBody] CreateUserBookDTO userBook)
    {
      if (userBook is null)
        return BadRequest("UserBookDTO object is null");
      var userBookToReturn = await _service.UserBookService.CreateUserBookAsync(userBook, trackChanges: false);
      if (userBookToReturn is null)
        return BadRequest("bookToReturn object is null");
      return Ok(userBookToReturn);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBooks()
    {
      var userBooks = await _service.UserBookService.GetAllUserBooksAsync(trackChanges: false);
      return Ok(userBooks);
    }

    [HttpGet("{bookId:Guid}/{userName}", Name = "GetUserBookById")]
    public async Task<IActionResult> GetUserBookById(Guid bookId, string userName)
    {
      var userBook = await _service.UserBookService.GetUserBookAsync(bookId, userName, trackChanges: false);
      return Ok(userBook);
    }

    [HttpDelete("{bookId:Guid}/{userName}")]
    public async Task<IActionResult> DeleteUserBook(Guid bookId, string userName)
    {
      await _service.UserBookService.DeleteUserBookAsync(bookId, userName, trackChanges: false);
      return NoContent();
    }

    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> DeleteUserBookById(Guid Id)
    {
      await _service.UserBookService.DeleteUserBookByIdAsync(Id, trackChanges: false);
      return NoContent();
    }
  }
}
