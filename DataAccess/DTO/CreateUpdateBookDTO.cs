using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
  public record CreateUpdateBookDTO(string ISBN, string Name, string Jenre, DateTime TakeTime, DateTime ReturnTime);
}
