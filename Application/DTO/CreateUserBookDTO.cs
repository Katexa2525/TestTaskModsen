﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DTO
{
    public class CreateUserBookDTO
    {
        public Guid IdBook { get; set; }
        public string IdUser { get; set; }
  }
}
