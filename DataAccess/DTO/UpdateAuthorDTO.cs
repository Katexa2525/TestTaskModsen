using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
  public record UpdateAuthorDTO(string Name, string Surname,DateTime BirthdayDate, string Country,IEnumerable<CreateAuthorDTO> Authors);

}
