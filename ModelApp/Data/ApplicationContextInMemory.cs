
using ModelApp.Models;

namespace ModelApp.Data;

public static class ApplicationContextInMemory
{
    public static List<Book> Books { get; set; }
    public static List<Category> Categories { get; set; }
    static ApplicationContextInMemory()
    {
        Books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", Price = 10.99m },
            new Book { Id = 2, Title = "1984", Price = 8.99m },
            new Book { Id = 3, Title = "To Kill a Mockingbird", Price = 12.50m }
        };

        Categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Fiction" },
            new Category { CategoryId = 2, CategoryName = "Science Fiction" },
            new Category { CategoryId = 3, CategoryName = "Classic" }
        };
    }
}