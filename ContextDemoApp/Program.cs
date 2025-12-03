using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnectionString")));


var app = builder.Build();

app.MapGet("/", (RepositoryContext context) =>
    context.Books.ToList()
);

app.Run();

public class Book
{
    public int Id { get; set; }
    public String Title { get; set; }
    public decimal Price { get; set; }
    public String Author { get; set; } = String.Empty;
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

        // Fluent API configurations
        modelBuilder.Entity<Book>()
            .ToTable("Books");

        modelBuilder.Entity<Book>()
            .Property(b => b.Author)
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Price = 10.99M,
                Author = "George Sell"
            },
            new Book
            {
                Id = 2,
                Title = "1984",
                Price = 8.99M,
                Author = "Michella Cash"
            },
            new Book
            {
                Id = 3,
                Title = "To Kill a Mockingbird",
                Price = 12.49M,
                Author = "Anne William"
            }
        );
    }
}