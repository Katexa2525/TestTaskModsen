using Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ModsenTestTask.ContextFactory
{
  public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
  {
    public RepositoryContext CreateDbContext(string[] args)
    {
      var configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .Build();
      var builder = new DbContextOptionsBuilder<RepositoryContext>().UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
      return new RepositoryContext(builder.Options);
    }
  }
}
