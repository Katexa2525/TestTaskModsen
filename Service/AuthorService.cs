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
    public async Task<AuthorDTO> CreateAuthorAsync(CreateAuthorDTO author)
    {
      var authorEntity = _mapper.Map<Author>(author);
      _repository.Author.CreateAuthor(authorEntity);
      await _repository.SaveAsync();
      var authorToReturn = _mapper.Map<AuthorDTO>(authorEntity);
      return authorToReturn;
    }
    public async Task DeleteAuthorAsync(Guid id, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges: false);
      if (author == null)
        throw new AuthorNotFoundException(id);     
      _repository.Author.DeleteAuthor(author);
      await _repository.SaveAsync();
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync(bool trackChanges)
    {
      var authors = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);
      var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
      return authorsDTO;
    }

    public async Task<AuthorDTO> GetAuthorAsync(Guid id, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges: false);
      if (author is null)
        throw new AuthorNotFoundException(id);

      var authorDTO = _mapper.Map<AuthorDTO>(author);
      return authorDTO;
    }

    public async Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges)
    {
      var authorEntity = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges);
      if (authorEntity is null)
        throw new AuthorNotFoundException(authorId);
      _mapper.Map(UpdateAuthor, authorEntity);
      await _repository.SaveAsync();
    }
  }
}
