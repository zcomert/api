using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Config;

public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Id = "41ac2c24-9ae5-4661-830b-a978f55b77b9", // Statik bir GUID
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "d477d82e-9f6f-49b6-9a26-ea3da7fb4680" // Statik bir GUID
            },
            new IdentityRole()
            {
                Id = "08a8d3ae-ed69-47b4-a744-1777a34c88e7", // Statik bir GUID
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "a8e28621-99ca-4ed0-b942-f45d7f6a336e" // Statik bir GUID
            }
         );
    }
}
