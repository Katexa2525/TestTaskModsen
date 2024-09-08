using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Jenre { get; set; }
        //public byte[] Image { get; set; }
        public DateTime TakeTime { get; set; }
        public DateTime ReturnTime { get; set; }
    }
}
