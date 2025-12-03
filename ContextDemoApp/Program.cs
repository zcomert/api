using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnectionString")));


var app = builder.Build();

app.MapGet("/", () => "Selam");

app.Run();

public class Book
{
    public int Id { get; set; }
    public String Title { get; set; }
    public decimal Price { get; set; }
}

public class RepositoryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "The Great Gatsby", Price = 10.99M },
            new Book { Id = 2, Title = "1984", Price = 8.99M },
            new Book { Id = 3, Title = "To Kill a Mockingbird", Price = 12.49M }
        );  
    }
}