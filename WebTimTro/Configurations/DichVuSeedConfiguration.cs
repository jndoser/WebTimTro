using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class DichVuSeedConfiguration : IEntityTypeConfiguration<DichVu>
    {
        public void Configure(EntityTypeBuilder<DichVu> builder)
        {
            builder.HasData(
                new DichVu
                {
                    Id = 1,
                    Ten = "Wifi"
                },
                new DichVu
                {
                    Id = 2,
                    Ten = "Máy giặc"
                },
                new DichVu
                {
                    Id = 3,
                    Ten = "Máy lạnh"
                },
                new DichVu
                {
                    Id = 4,
                    Ten = "Tủ lạnh"
                },
                new DichVu
                {
                    Id = 5,
                    Ten = "TV với truyền hình cáp tiêu chuẩn"
                },
                new DichVu
                {
                    Id = 6,
                    Ten = "Máy sấy quần áo"
                },
                new DichVu
                {
                    Id = 7,
                    Ten = "Camera an ninh trong nhà"
                });
        }
    }
}
