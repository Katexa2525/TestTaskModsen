using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestFeatures
{
  public class BookParameters: RequestParameters
  {
    public string? SearchTerm { get; set; }
  }
}
