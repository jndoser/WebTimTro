using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroLuuTruConfiguration : IEntityTypeConfiguration<PhongTroLuuTru>
    {
        public void Configure(EntityTypeBuilder<PhongTroLuuTru> builder)
        {
            builder.HasKey(x => new { x.NguoiDungId, x.PhongTroId });

            // Configure quan hệ 1 - n giữa NguoiDung và PhongTroLuuTru
            builder.HasOne(x => x.NguoiDung)
                .WithMany(y => y.PhongTroLuuTrus)
                .HasForeignKey(x => x.NguoiDungId);

            // Configure quan hệ 1 - n giữa PhongTro và PhongTroLuuTru
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroLuuTrus)
                .HasForeignKey(x => x.PhongTroId);
        }
    }
}
