using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ModsenTestTask.Controllers
{
  [Route("api/authors")]
  [ApiController]
  public class AuthorsController : ControllerBase
  {
    private readonly IServiceManager _service;

    public AuthorsController(IServiceManager service)
    {
      _service = service;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAuthors() 
    {
      var authors = _service.AuthorService.GetAllAuthors(trackChanges:false);
      return Ok(authors);
    }

    [HttpGet("{id: guid}", Name = "GetAuthorById")]
    public IActionResult GetAuthorById(Guid id) 
    {
      var author = _service.AuthorService.GetAuthor(id, trackChanges: false);
      return Ok(author);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteAuthor(Guid id)
    {
      _service.AuthorService.DeleteAuthor(id, trackChanges: false);
      return NoContent();
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorDTO author)
    {
      if (author is null)
        return BadRequest("CreateAuthorDTO object is null");
      var createdAuthor = _service.AuthorService.CreateAuthor(author);
      return CreatedAtRoute("AuthorById", new { id = createdAuthor.Id },createdAuthor);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO author)
    {
      if (author is null)
        return BadRequest("UpdateAuthorDTO object is null");
      _service.AuthorService.UpdateAuthor(id, author, trackChanges: true);
      return NoContent();
    }
  }
}
