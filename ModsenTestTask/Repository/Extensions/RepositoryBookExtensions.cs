using Domain.Entities.Models;

namespace Infrastructure.Extensions
{
  public static class RepositoryBookExtensions
  {
    public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
    {
      if (string.IsNullOrWhiteSpace(searchTerm))
        return books;
      var lowerCaseTerm = searchTerm.Trim().ToLower();
      return books.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

  }
}
