using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroDanhGiaConfiguration : IEntityTypeConfiguration<PhongTroDanhGia>
    {
        public void Configure(EntityTypeBuilder<PhongTroDanhGia> builder)
        {
            builder.HasKey(x => new { x.NguoiDungId, x.PhongTroId });

            // Configure mối quan hệ 1 - n giữa PhongTroDanhGia và NguoiDung
            builder.HasOne(x => x.NguoiDung)
                .WithMany(y => y.PhongTroDanhGias)
                .HasForeignKey(x => x.NguoiDungId);

            // Configure mối quan hệ 1 - n giữa PhongTroDanhGia và PhongTro
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroDanhGias)
                .HasForeignKey(x => x.PhongTroId);
        }
    }
}
