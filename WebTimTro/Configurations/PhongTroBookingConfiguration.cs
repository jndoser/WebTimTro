using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroBookingConfiguration : IEntityTypeConfiguration<PhongTroBooking>
    {
        public void Configure(EntityTypeBuilder<PhongTroBooking> builder)
        {
            // Set primary key cho bảng Phòng Trọ Booking
            builder.HasKey(x => new { x.PhongTroId, x.NguoiDungId });

            // Configure relationship 1 - n Người dùng được
            // book nhiều phòng trọ, nhưng trong 1 thời điểm
            // chỉ được book 1 phòng trọ duy nhất
            builder.HasOne(x => x.NguoiDung)
                .WithMany(y => y.PhongTroBookings)
                .HasForeignKey(x => x.NguoiDungId);

            // Configure relationship 1 -n Phòng trọ có thể thuộc
            // nhiều booking khác nhau
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroBookings)
                .HasForeignKey(x => x.PhongTroId);
        }
    }
}
