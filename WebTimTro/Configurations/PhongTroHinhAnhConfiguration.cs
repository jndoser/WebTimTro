using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroHinhAnhConfiguration
        : IEntityTypeConfiguration<PhongTroHinhAnh>
    {
        public void Configure(EntityTypeBuilder<PhongTroHinhAnh> builder)
        {
            builder.HasKey(x => new { x.PhongTroId, x.HinhAnhId });

            // Configure mối quan hệ giữa bảng PhongTroHinhAnh 
            // và 2 bảng Phòng Trọ, Hình Ảnh
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroHinhAnhs)
                .HasForeignKey(x => x.PhongTroId);

            builder.HasOne(x => x.HinhAnh)
                .WithMany(y => y.PhongTroHinhAnhs)
                .HasForeignKey(x => x.HinhAnhId);
        }
    }
}
