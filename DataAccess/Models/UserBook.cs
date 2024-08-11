using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
  public class UserBook
  {
    [Column("UserBookId")]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Book))]
    public Guid IdBook { get; set; }
    public Book Book { get; set; }

    [ForeignKey(nameof(User))]
    public string IdUser { get; set; }
    public User? User { get; set; }
  }
}
