using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
  public class RepositoryContext: IdentityDbContext<User>
  {
    public RepositoryContext(DbContextOptions options): base(options) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
  }
}
