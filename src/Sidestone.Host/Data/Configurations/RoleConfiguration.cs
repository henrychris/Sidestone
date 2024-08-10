using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sidestone.Host.Data.Enums;

namespace Sidestone.Host.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = Roles.CommunityManager.ToString(),
                    NormalizedName = Roles.CommunityManager.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = Roles.Resident.ToString(),
                    NormalizedName = Roles.Resident.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = Roles.Admin.ToString(),
                    NormalizedName = Roles.Admin.ToString().ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }
    }
}
