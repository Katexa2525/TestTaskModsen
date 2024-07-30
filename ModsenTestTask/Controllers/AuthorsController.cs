using Contracts;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ModsenTestTask.Controllers
{
  [Route("api/authors")]
  [ApiController]
  public class AuthorsController : ControllerBase
  {
    private readonly IRepositoryManager _repository;

    public AuthorsController(IRepositoryManager repository)
    {
      _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAuthors() 
    {
      try 
      {
        var authors = _repository.Author.GetAllAuthors(trackChanges:false);
        var authorsDTO = authors.Select(c => new AuthorDTO
        {
          Id = c.Id,
          Name = c.Name,
          Surname = c.Surname,
          BirthdayDate = c.BirthdayDate,
          Country = c.Country,
        }).ToList();
        return Ok(authorsDTO);
      }
      catch (Exception ex) 
      {
        return StatusCode(500, "Internal server error");
      }
    }
  }
}
