using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebTimTro.Configurations
{
    public class RolesSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "admin",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "nguoidung",
                    Name = "Nguoidung",
                    NormalizedName = "NGUOIDUNG"
                },
                new IdentityRole
                {
                    Id = "chutro",
                    Name = "Chutro",
                    NormalizedName = "CHUTRO"
                }
                );
        }
    }
}
