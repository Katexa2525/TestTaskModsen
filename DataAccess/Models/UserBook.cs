﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
    public Guid IdUser { get; set; }
    public User User { get; set; }
  }
}
