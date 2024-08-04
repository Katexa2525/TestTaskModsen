using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    public AuthorDTO CreateAuthor(CreateAuthorDTO author)
    {
      var authorEntity = _mapper.Map<Author>(author);
      _repository.Author.CreateAuthor(authorEntity);
      _repository.Save();
      var authorToReturn = _mapper.Map<AuthorDTO>(authorEntity);
      return authorToReturn;
    }
    public void DeleteAuthor(Guid id, bool trackChanges)
    {
      var author = _repository.Author.GetAuthorById(id, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(id);     
      _repository.Author.DeleteAuthor(author);
      _repository.Save();
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

    public void UpdateAuthor(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges)
    {
      var authorEntity = _repository.Author.GetAuthorById(authorId, trackChanges);
      if (authorEntity is null)
        throw new AuthorNotFoundException(authorId);
      _mapper.Map(UpdateAuthor, authorEntity);
      _repository.Save();
    }
  }
}
