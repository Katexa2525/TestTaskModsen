using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities.Validation;
using Domain.Entities.Exceptions;
using Application.Mapping;

namespace Application.Services
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
      var validator = new AuthorValidator();

      Author authorEntity = AuthorMapping.ToAuthor(author);

      /*FluentValidation.Results.ValidationResult*/
      var validationResult = validator.Validate(authorEntity);
      if (validationResult.IsValid)
      {
        _repository.Author.CreateAuthor(authorEntity);
        await _repository.SaveAsync();

        AuthorDTO authorToReturn = AuthorMapping.ToAuthorResponse(authorEntity);
        return authorToReturn;
      }
      return null;
    }
    public async Task DeleteAuthorAsync(Guid id, bool trackChanges)
    {
      var validator = new AuthorValidator();
      var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges: false);
      var validationResult = validator.Validate(author);
      if (validationResult.IsValid)
      {
        if (author == null)
          throw new AuthorNotFoundException(id);
        _repository.Author.DeleteAuthor(author);
        await _repository.SaveAsync();
      }
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync(bool trackChanges)
    {
      var authors = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);
      IEnumerable<AuthorDTO> authorToReturn = authors.Select(authors => authors.ToAuthorResponse());
      return authorToReturn;
    }

    public async Task<AuthorDTO> GetAuthorAsync(Guid id, bool trackChanges)
    {
      var author = await _repository.Author.GetAuthorByIdAsync(id, trackChanges: false);
      if (author is null)
        throw new AuthorNotFoundException(id);

      //var authorDTO = new AuthorDTO
      //{
      //  Id = author.Id,
      //  BirthdayDate = author.BirthdayDate,
      //  Country = author.Country,
      //  Name = author.Name,
      //  Surname = author.Surname,
      //};
      return author.ToAuthorResponse();
    }

    public async Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges)
    {
      var validator = new AuthorValidator();

      var authorEntity = await _repository.Author.GetAuthorByIdAsync(authorId, trackChanges);
      var validationResult = validator.Validate(authorEntity);
      if (authorEntity is null)
        throw new AuthorNotFoundException(authorId);
      if (validationResult.IsValid)
      {
        //authorEntity.Name = UpdateAuthor.Name;
        //authorEntity.Surname = UpdateAuthor.Surname;
        //authorEntity.BirthdayDate = UpdateAuthor.BirthdayDate;
        //authorEntity.Country = UpdateAuthor.Country;

        authorEntity = AuthorMapping.ToAuthor(UpdateAuthor, authorEntity);

        await _repository.SaveAsync();
      }
    }
  }
}
