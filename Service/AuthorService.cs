using AutoMapper;
using Contracts;
using Contracts.Services;
using Entities.DTO;
using Entities.Exceptions;
using Entities.Models;
using System.Collections.Generic;
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
        //var authorEntity = _mapper.Map<Author>(author);
        Author authorEntity = new Author
        {
          BirthdayDate = author.BirthdayDate,
          Country = author.Country,
          Name = author.Name,
          Surname = author.Surname,
          Id = Guid.NewGuid()
        };
        _repository.Author.CreateAuthor(authorEntity);
        await _repository.SaveAsync();
      AuthorDTO authorToReturn = new AuthorDTO
      {
        Id = authorEntity.Id,
        BirthdayDate = authorEntity.BirthdayDate,
        Country = authorEntity.Country,
        Name = authorEntity.Name,
        Surname = authorEntity.Surname,
      };//_mapper.Map<AuthorDTO>(authorEntity);
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
      //var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
      IEnumerable<AuthorDTO> authorToReturn = authors.Select(authors => new AuthorDTO
      {
        Id = authors.Id,
        BirthdayDate = authors.BirthdayDate,
        Country = authors.Country,
        Name = authors.Name,
        Surname = authors.Surname,
      });
      return authorToReturn;
    }

    public async Task<AuthorDTO> GetAuthorAsync(Guid id, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges: false);
      if (author is null)
        throw new AuthorNotFoundException(id);

      var authorDTO = new AuthorDTO
      {
        Id = author.Id,
        BirthdayDate = author.BirthdayDate,
        Country = author.Country,
        Name = author.Name,
        Surname = author.Surname,
      };//_mapper.Map<AuthorDTO>(author);
      return authorDTO;
    }

    public async Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges)
    {
      var authorEntity = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges);
      if (authorEntity is null)
        throw new AuthorNotFoundException(authorId);

      authorEntity.Name = UpdateAuthor.Name;
      authorEntity.Surname = UpdateAuthor.Surname;
      authorEntity.BirthdayDate = UpdateAuthor.BirthdayDate;
      authorEntity.Country = UpdateAuthor.Country;
     
      //_mapper.Map(UpdateAuthor, authorEntity);
      await _repository.SaveAsync();
    }
  }
}
