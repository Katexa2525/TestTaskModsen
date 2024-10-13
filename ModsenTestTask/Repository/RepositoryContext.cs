using Domain.Entities.Models;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class RepositoryContext : IdentityDbContext<User>
  {
    public RepositoryContext(DbContextOptions options) : base(options)
    {
      Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("public");
      modelBuilder.Entity<Book>().Property(b => b.Id).HasColumnName("BookId");
      modelBuilder.Entity<Author>().Property(a => a.Id).HasColumnName("AuthorId");
      modelBuilder.Entity<UserBook>().Property(ub => ub.Id).HasColumnName("UserBookId");

      modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne().HasForeignKey(a => a.IdAuthor);
      modelBuilder.Entity<Book>().HasMany(a => a.UserBooks).WithOne().HasForeignKey(a => a.IdBook);

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<UserBook>? UserBook { get; set; }
  }
}
