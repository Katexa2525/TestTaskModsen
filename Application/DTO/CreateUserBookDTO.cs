using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.DTO
{
  public class CreateUserBookDTO
  {
    public Guid IdBook { get; set; }
    public string IdUser { get; set; }
    public string UserName { get; set; }
  }
}
