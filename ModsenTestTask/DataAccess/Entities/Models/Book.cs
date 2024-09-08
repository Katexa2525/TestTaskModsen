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
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string? Jenre { get; set; }
        public byte[]? Image { get; set; }
        public DateTime TakeTime { get; set; }
        public DateTime ReturnTime { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid IdAuthor { get; set; }
        public Author? Author { get; set; }

        public ICollection<UserBook>? UserBooks { get; set; }
    }
}
