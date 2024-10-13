using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models
{
  public class Book
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string? Jenre { get; set; }
        public byte[]? Image { get; set; }
        public DateTime TakeTime { get; set; }
        public DateTime ReturnTime { get; set; }

        public Guid IdAuthor { get; set; }
        public ICollection<UserBook>? UserBooks { get; set; }
    }
}
