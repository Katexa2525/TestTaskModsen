using Entities.Models;

namespace Entities.DTO
{
  public class AuthorDTO
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthdayDate { get; set; }
    public string Country { get; set; }
  }
}
