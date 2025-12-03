var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new List<Book>()
{
    new Book { Id = 1, Title = "The Great Gatsby", Price = 10.99M },
    new Book { Id = 2, Title = "1984", Price = 8.99M },
    new Book { Id = 3, Title = "To Kill a Mockingbird", Price = 12.49M }
});

app.Run();

class Book
{
    public int Id { get; set; }
    public String Title { get; set; }
    public decimal Price { get; set; }
}