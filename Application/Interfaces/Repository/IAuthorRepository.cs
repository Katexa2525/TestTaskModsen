using Domain.Entities.Models;

namespace Application.Interfaces.Repository
{
  public interface IAuthorRepository
  {
    Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorByIdAsync(Guid authorId, bool trackChanges);
    void DeleteAuthor(Author author);
    void CreateAuthor(Author author);
  }
}
