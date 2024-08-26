using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models
{
    public class Book
    {
        [Column("BookId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ISBN is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the ISBN is 50 characters.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Book name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Book jenre is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Jenre is 30 characters.")]
        public string? Jenre { get; set; }
        public byte[]? Image { get; set; }

        [Required(ErrorMessage = "Book TakeTime is a required field.")]
        public DateTime TakeTime { get; set; }

        [Required(ErrorMessage = "Book ReturnTime is a required field.")]
        public DateTime ReturnTime { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid IdAuthor { get; set; }
        public Author? Author { get; set; }

        public ICollection<UserBook>? UserBooks { get; set; }
    }
}
