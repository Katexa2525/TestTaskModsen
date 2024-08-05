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
  //public class Helper
  //{
  //  public static string ToUnderscoreCase(string str)
  //  {
  //    return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
  //  }
  //}
  public class RepositoryContext: IdentityDbContext<User>
  {
    public RepositoryContext(DbContextOptions options): base(options) 
    {
      //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      base.OnModelCreating(modelBuilder);
      //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
      //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
      //foreach (var entity in modelBuilder.Model.GetEntityTypes())
      //{
      //  var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
      //  if (currentTableName.Contains("<"))
      //  {
      //    currentTableName = currentTableName.Split('<')[0];
      //  }
      //  modelBuilder.Entity(entity.Name).ToTable(Helper.ToUnderscoreCase(currentTableName));
      //}
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
  }
}
