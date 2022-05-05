using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroQuanTamConfiguration : IEntityTypeConfiguration<PhongTroQuanTam>
    {
        public void Configure(EntityTypeBuilder<PhongTroQuanTam> builder)
        {
            builder.HasKey(x => new { x.NguoiDungId, x.PhongTroId });

            // Configure quan hệ 1 - n giữa NguoiDung và PhongTroQuanTam
            builder.HasOne(x => x.NguoiDung)
                .WithMany(y => y.PhongTroQuanTams)
                .HasForeignKey(x => x.NguoiDungId);

            // Configure quan hệ 1 - n giữa PhongTro và PhongTroQuanTam
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroQuanTams)
                .HasForeignKey(x => x.PhongTroId);
        }
    }
}
