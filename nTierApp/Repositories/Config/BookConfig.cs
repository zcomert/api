
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Repositories.Config;

public partial class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasData(
                new Book()
                {
                    Id = 1,
                    Title = "Clean Code",
                    Price = 29.99M
                },
                new Book()
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Price = 39.99M
                },
                new Book()
                {
                    Id = 3,
                    Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
                    Price = 49.99M
                }
                );
    }
}
