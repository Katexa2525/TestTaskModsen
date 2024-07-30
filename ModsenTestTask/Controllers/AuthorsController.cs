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
      try 
      {
        var authors = _repository.Author.GetAllAuthors(trackChanges:false);
        var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        return Ok(authorsDTO);
      }
      catch (Exception ex) 
      {
        _logger.LogError($"Something went wrong in the {nameof(GetAuthors)} action {ex}");
        return StatusCode(500, "Internal server error");
      }
    }
  }
}
