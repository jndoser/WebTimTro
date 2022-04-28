using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroNoteConfiguration : IEntityTypeConfiguration<PhongTroNote>
    {
        public void Configure(EntityTypeBuilder<PhongTroNote> builder)
        {
            // Set Primary key cho bảng Phòng Trọ - Ghi Chú
            builder.HasKey(x => new { x.PhongTroId, x.NoteId });

            // Set relationship của bảng Phòng Trọ - Ghi Chú
            // với bảng Phòng Trọ
            builder.HasOne(x => x.PhongTro)
                .WithMany(y => y.PhongTroNotes)
                .HasForeignKey(x => x.PhongTroId);

            // Set relationship của bảng Phòng Trọ - Ghi Chú
            // với bảng Ghi Chú
            builder.HasOne(x => x.Note)
                .WithMany(y => y.PhongTroNotes)
                .HasForeignKey(x => x.NoteId);
        }
    }
}
