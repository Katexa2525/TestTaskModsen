using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Models
{
    public class UserBook
    {
        public Guid Id { get; set; }
        public Guid IdBook { get; set; }
        public string IdUser { get; set; }
    }
}
