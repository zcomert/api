using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Repositories;

public class RepositoryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new BookConfig());
        //modelBuilder.ApplyConfiguration(new CategoryConfig());

        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
    }
}
