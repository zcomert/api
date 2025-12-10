using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;


namespace Repositories;

public class RepositoryContext : IdentityDbContext<AppUser>
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new BookConfig());
        //modelBuilder.ApplyConfiguration(new CategoryConfig());
        //modelBuilder.ApplyConfiguration(new RoleConfig());
        
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(RepositoryContext).Assembly);
    }
}
