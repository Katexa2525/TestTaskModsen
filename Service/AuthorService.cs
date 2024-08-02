using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;

namespace Service
{
  internal sealed class AuthorService: IAuthorService
  {
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public AuthorService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    public IEnumerable<AuthorDTO> GetAllAuthors(bool trackChanges)
    {
      var authors = _repository.Author.GetAllAuthors(trackChanges: false);
      var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
      return authorsDTO;
    }

    public AuthorDTO GetAuthor(Guid id, bool trackChanges)
    {
      var author = _repository.Author.GetAuthorById(id, trackChanges: false);
      if (author is null)
        throw new AuthorNotFoundException(id);

      var authorDTO = _mapper.Map<AuthorDTO>(author);
      return authorDTO;
    }
  }
}
