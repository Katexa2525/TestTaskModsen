using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
  public class UserBookDTO
  {
    public Guid Id { get; set; }
    public Guid IdBook { get; set; }
    public string IdUser { get; set; }
  }
}
