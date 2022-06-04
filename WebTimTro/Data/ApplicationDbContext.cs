using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebTimTro.Configurations;

namespace WebTimTro.Data
{
    public class ApplicationDbContext : IdentityDbContext<NguoiDung>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<PhongTro> PhongTros { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<PhongTroDichVu> PhongTroDichVus { get; set; }
        public DbSet<PhongTroNote> PhongTroNotes { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<PhongTroHinhAnh> PhongTroHinhAnhs { get; set; }
        public DbSet<PhongTroLuuTru> PhongTroLuuTrus { get; set; }
        public DbSet<PhongTroQuanTam> PhongTroQuanTams { get; set; }
        public DbSet<PhongTroBooking> PhongTroBookings { get; set; }
        public DbSet<PhongTroDanhGia> PhongTroDanhGias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BinhLuanConfiguration());
            builder.ApplyConfiguration(new PhongTroDichVuConfiguration());
            builder.ApplyConfiguration(new PhongTroNoteConfiguration());
            builder.ApplyConfiguration(new RolesSeedConfiguration());
            builder.ApplyConfiguration(new PhongTroConfiguration());
            builder.ApplyConfiguration(new PhongTroHinhAnhConfiguration());
            builder.ApplyConfiguration(new DichVuSeedConfiguration());
            builder.ApplyConfiguration(new PhongTroLuuTruConfiguration());
            builder.ApplyConfiguration(new PhongTroQuanTamConfiguration());
            builder.ApplyConfiguration(new PhongTroBookingConfiguration());
            builder.ApplyConfiguration(new PhongTroDanhGiaConfiguration());


            base.OnModelCreating(builder);
        }
    }
}
