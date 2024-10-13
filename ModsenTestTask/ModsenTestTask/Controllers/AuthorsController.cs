using Application.UseCases.Commands;
using Application.Interfaces.Services;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
  [Route("api/authors")]
  [ApiController]
  public class AuthorsController : ControllerBase
  {

    private readonly ISender _sender;
    public AuthorsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAuthors() 
    {
      var authors = await _sender.Send(new GetAuthorsQuery(TrackChanges: false));
      return Ok(authors);
    }

    [HttpGet("{id:Guid}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorById(Guid id) 
    {
      var author = await _sender.Send(new GetAuthorByIdQuery(id, trackChanges: false));
      return Ok(author);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
      await _sender.Send(new DeleteAuthorCommand(id, trackChanges: false));
      return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO author)
    {
      var createdAuthor = await _sender.Send(new CreateAuthorCommand(author));
      return CreatedAtRoute("GetAuthorById", new { id = createdAuthor.Id },createdAuthor);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO author)
    {
      await _sender.Send(new UpdateAuthorCommand(id, author, trackChanges: true));
      return NoContent();
    }
  }
}
