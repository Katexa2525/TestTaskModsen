using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Models;
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

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(Guid id)
    {
      var author = _repository.Author.GetAuthorById(id, trackChanges: false);
      if (author == null)
      {
        _logger.LogInfo($"Author with id: {id} doesn't exist in the database.");
        return NotFound();
      }
      _repository.Author.DeleteAuthor(author);
      _repository.Save();
      return NoContent();
    }

    // izmenit
    [HttpPost]
    public IActionResult CreateCompany([FromBody] AuthorDTO author)
    {
      if (author == null)
      {
        _logger.LogError("AuthorDTO object sent from client is null.");
        return BadRequest("AuthorDTO object is null");
      }
      var authorEntity = _mapper.Map<Author>(author);
      _repository.Author.CreateAuthor(authorEntity);
      _repository.Save();
      var AuthorToReturn = _mapper.Map<AuthorDTO>(authorEntity);
      return CreatedAtRoute("AuthorById", new { id = AuthorToReturn.Id }, AuthorToReturn);
    }
  }
}
