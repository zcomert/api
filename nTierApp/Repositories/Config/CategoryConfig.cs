
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(c => c.CagetoryName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Category { CategoryId = 1, CagetoryName = "Science Fiction" },
            new Category { CategoryId = 2, CagetoryName = "Fantasy" },
            new Category { CategoryId = 3, CagetoryName = "Mystery" },
            new Category { CategoryId = 4, CagetoryName = "Romance" },
            new Category { CategoryId = 5, CagetoryName = "Horror" }
        );
    }
}
