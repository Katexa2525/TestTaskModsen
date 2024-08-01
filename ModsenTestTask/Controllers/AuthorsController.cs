using AutoMapper;
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
    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public AuthorsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors() 
    {
      var authors = _repository.Author.GetAllAuthors(trackChanges:false);
      var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
      return Ok(authorsDTO);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(Guid id) 
    {
      var author = _repository.Author.GetAuthorById(id, trackChanges: false);
      if (author == null) 
      {
        _logger.LogInfo($"Author with id: {id} doesn't exist in the database.");
        return NotFound();
      }
      else 
      {
        var authorDTO = _mapper.Map<AuthorDTO>(author);
        return Ok(authorDTO);
      }
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
  }
}
