using Entities.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
  public class Author
  {
    [Column("AuthorId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Author name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Author surname is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Surname is 30 characters.")]
    public string Surname { get; set; }

    public DateTime BirthdayDate { get; set; }

    public string Country { get; set; }
    
    public ICollection<Book>? Books { get; set; }  
  }
}
