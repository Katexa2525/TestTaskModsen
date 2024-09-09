﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public record CreateAuthorDTO(string Name, string Surname, DateTime BirthdayDate, string Country);
}
