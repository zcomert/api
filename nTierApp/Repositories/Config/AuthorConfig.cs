using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Config;

public class AuthorConfig : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => a.AuthorId);
        
        builder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasData(
            new Author { AuthorId = 1, FirstName = "George", LastName = "Orwell" },
            new Author { AuthorId = 2, FirstName = "Jane", LastName = "Austen" },
            new Author { AuthorId = 3, FirstName = "Mark", LastName = "Twain" }
        );
    }
}
