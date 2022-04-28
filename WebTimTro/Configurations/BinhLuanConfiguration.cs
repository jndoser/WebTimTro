using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class BinhLuanConfiguration : IEntityTypeConfiguration<BinhLuan>
    {
        public void Configure(EntityTypeBuilder<BinhLuan> builder)
        {
            // Set relationship giữa bình luận con
            // và bình luận cha
            builder.HasOne(x => x.BinhLuanCha)
                .WithMany(x => x.BinhLuanCons)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Set relationship giữa bình luận và
            // người dùng
            builder.HasOne(x => x.NguoiDung)
                .WithMany(y => y.BinhLuans)
                .HasForeignKey(x => x.NguoiDungId);

            // Set relationship giữa bình luận và
            // phòng trọ
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.BinhLuans)
                .HasForeignKey(x => x.PhongTroId);
        }
    }
}
