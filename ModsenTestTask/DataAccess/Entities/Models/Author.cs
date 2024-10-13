using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models
{
  public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime BirthdayDate { get; set; }

        public string Country { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
