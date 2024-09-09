using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public record CreateUpdateBookDTO(string ISBN, string Name, string Jenre, IFormFile Image, DateTime TakeTime, DateTime ReturnTime);
}
