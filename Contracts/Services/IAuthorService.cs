using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracts.Services
{
  public interface IAuthorService
  {
    IEnumerable<AuthorDTO> GetAllAuthors(bool trackChanges);
    AuthorDTO GetAuthor(Guid id, bool trackChanges);
  }
}
