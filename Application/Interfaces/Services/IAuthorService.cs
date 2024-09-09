using Domain.Entities.DTO;

namespace Application.Interfaces.Services
{
  public interface IAuthorService
  {
    Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync(bool trackChanges);
    Task<AuthorDTO> GetAuthorAsync(Guid id, bool trackChanges);
    Task DeleteAuthorAsync(Guid id, bool trackChanges);
    Task<AuthorDTO> CreateAuthorAsync(CreateAuthorDTO author);
    Task UpdateAuthorAsync(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges);

  }
}
