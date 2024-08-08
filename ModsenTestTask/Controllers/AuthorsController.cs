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
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetAuthors() 
    {
      var authors = await _service.AuthorService.GetAllAuthorsAsync(trackChanges:false);
      return Ok(authors);
    }

    [HttpGet("{id:Guid}", Name = "GetAuthorById")]
    public async Task<IActionResult> GetAuthorById(Guid id) 
    {
      var author = await _service.AuthorService.GetAuthorAsync(id, trackChanges: false);
      return Ok(author);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
      await _service.AuthorService.DeleteAuthorAsync(id, trackChanges: false);
      return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO author)
    {
      if (author is null)
        return BadRequest("CreateAuthorDTO object is null");
      var createdAuthor = await _service.AuthorService.CreateAuthorAsync(author);
      return CreatedAtRoute("AuthorById", new { id = createdAuthor.Id },createdAuthor);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO author)
    {
      if (author is null)
        return BadRequest("UpdateAuthorDTO object is null");
      await _service.AuthorService.UpdateAuthorAsync(id, author, trackChanges: true);
      return NoContent();
    }
  }
}
