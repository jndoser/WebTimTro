﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTimTro.Data;

namespace WebTimTro.Configurations
{
    public class PhongTroConfiguration : IEntityTypeConfiguration<PhongTro>
    {
        public void Configure(EntityTypeBuilder<PhongTro> builder)
        {
            // Configure mối quan hệ giữa chủ trọ và phòng trọ
            // 1 phòng trọ chỉ do 1 chủ trọ đăng và 1 chủ trọ có thể 
            // đăng bài về nhiều phòng trọ 
            builder.HasOne(x => x.ChuTro)
                .WithMany(y => y.PhongTros)
                .HasForeignKey(x => x.ChuTroId);
        }
    }
}
