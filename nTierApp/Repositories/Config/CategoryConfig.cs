
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(c => c.CategoryName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Category { CategoryId = 1, CategoryName = "Science Fiction" },
            new Category { CategoryId = 2, CategoryName = "Fantasy" },
            new Category { CategoryId = 3, CategoryName = "Mystery" },
            new Category { CategoryId = 4, CategoryName = "Romance" },
            new Category { CategoryId = 5, CategoryName = "Horror" }
        );
    }
}
