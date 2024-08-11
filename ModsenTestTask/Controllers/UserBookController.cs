using Contracts.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ModsenTestTask.Controllers
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
    public async Task<IActionResult> CrateUserBook([FromBody] UserBookDTO userBook)
    {
      if (userBook is null)
        return BadRequest("UserBookDTO object is null");
      var userBookToReturn = await _service.UserBookService.CreateUserBookAsync(userBook, trackChanges: false);
      if (userBookToReturn is null)
        return BadRequest("bookToReturn object is null");
      return CreatedAtRoute("GetUserBookById", new
      {
        userBook.IdBook,
        userBook.IdUser,
      },
      userBookToReturn);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBooks()
    {
      var userBooks = await _service.UserBookService.GetAllUserBooksAsync(trackChanges: false);
      return Ok(userBooks);
    }

    [HttpGet("{bookId:Guid}/{userId:Guid}", Name = "GetUserBookById")]
    public async Task<IActionResult> GetUserBookById(Guid bookId, Guid userId)
    {
      var userBook = await _service.UserBookService.GetUserBookAsync(bookId, userId, trackChanges: false);
      return Ok(userBook);
    }

    [HttpDelete("{bookId:Guid}/{userId:Guid}")]
    public async Task<IActionResult> DeleteUserBook(Guid bookId, Guid userId)
    {
      await _service.UserBookService.DeleteUserBookAsync(bookId, userId, trackChanges: false);
      return NoContent();
    }
  }
}
