using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
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
