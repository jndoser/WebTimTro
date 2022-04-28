using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroDichVuConfiguration : IEntityTypeConfiguration<PhongTroDichVu>
    {
        public void Configure(EntityTypeBuilder<PhongTroDichVu> builder)
        {
            // Set primary key cho bảng Phòng Trọ - Dịch vụ
            builder.HasKey(x => new { x.PhongTroId, x.DichVuId });

            // Set up relationship giữa bảng Phòng Trọ - Dịch vụ 
            // và Phòng Trọ
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroDichVus)
                .HasForeignKey(x => x.PhongTroId);

            // Set up relationshop giữa bảng Phòng Trọ - Dịch vụ
            // và Dịch Vụ
            builder.HasOne(x => x.DichVu)
                .WithMany(y => y.PhongTroDichVus)
                .HasForeignKey(x => x.DichVuId);
        }
    }
}
