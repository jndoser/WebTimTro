﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTimTro.Data;

namespace WebTimTro.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220603000508_AddPhongTroDanhGiaTable")]
    partial class AddPhongTroDanhGiaTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "admin",
                            ConcurrencyStamp = "4ccecb8c-d246-40f4-8767-429606013728",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "nguoidung",
                            ConcurrencyStamp = "31ad5b44-5a94-47f4-9d13-ffde94e384ff",
                            Name = "Nguoidung",
                            NormalizedName = "NGUOIDUNG"
                        },
                        new
                        {
                            Id = "chutro",
                            ConcurrencyStamp = "77a42cb3-04dc-4489-b91a-31ad640544a0",
                            Name = "Chutro",
                            NormalizedName = "CHUTRO"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebTimTro.Data.BinhLuan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsReported")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool?>("IsShow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<int>("RootId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuotLike")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NguoiDungId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PhongTroId");

                    b.ToTable("BinhLuans");
                });

            modelBuilder.Entity("WebTimTro.Data.DichVu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DichVus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ten = "Wifi"
                        },
                        new
                        {
                            Id = 2,
                            Ten = "Máy giặc"
                        },
                        new
                        {
                            Id = 3,
                            Ten = "Máy lạnh"
                        },
                        new
                        {
                            Id = 4,
                            Ten = "Tủ lạnh"
                        },
                        new
                        {
                            Id = 5,
                            Ten = "TV với truyền hình cáp tiêu chuẩn"
                        },
                        new
                        {
                            Id = 6,
                            Ten = "Máy sấy quần áo"
                        },
                        new
                        {
                            Id = 7,
                            Ten = "Camera an ninh trong nhà"
                        });
                });

            modelBuilder.Entity("WebTimTro.Data.HinhAnh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Filename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HinhAnhs");
                });

            modelBuilder.Entity("WebTimTro.Data.NguoiDung", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ChuTroRegisterStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Intro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Profile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegristeredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebTimTro.Data.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NoiDung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChuTroId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Gia")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("decimal(24,19)");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("decimal(24,19)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayHetHan")
                        .HasColumnType("datetime2");

                    b.Property<int>("SucChua")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThaiDang")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ChuTroId");

                    b.ToTable("PhongTros");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroBooking", b =>
                {
                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayDatTro")
                        .HasColumnType("datetime2");

                    b.HasKey("PhongTroId", "NguoiDungId");

                    b.HasIndex("NguoiDungId");

                    b.ToTable("PhongTroBookings");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroDanhGia", b =>
                {
                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("NguoiDungId", "PhongTroId");

                    b.HasIndex("PhongTroId");

                    b.ToTable("PhongTroDanhGias");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroDichVu", b =>
                {
                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<int>("DichVuId")
                        .HasColumnType("int");

                    b.HasKey("PhongTroId", "DichVuId");

                    b.HasIndex("DichVuId");

                    b.ToTable("PhongTroDichVus");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroHinhAnh", b =>
                {
                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<int>("HinhAnhId")
                        .HasColumnType("int");

                    b.HasKey("PhongTroId", "HinhAnhId");

                    b.HasIndex("HinhAnhId");

                    b.ToTable("PhongTroHinhAnhs");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroLuuTru", b =>
                {
                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.HasKey("NguoiDungId", "PhongTroId");

                    b.HasIndex("PhongTroId");

                    b.ToTable("PhongTroLuuTrus");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroNote", b =>
                {
                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.HasKey("PhongTroId", "NoteId");

                    b.HasIndex("NoteId");

                    b.ToTable("PhongTroNotes");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroQuanTam", b =>
                {
                    b.Property<string>("NguoiDungId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PhongTroId")
                        .HasColumnType("int");

                    b.HasKey("NguoiDungId", "PhongTroId");

                    b.HasIndex("PhongTroId");

                    b.ToTable("PhongTroQuanTams");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebTimTro.Data.BinhLuan", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "NguoiDung")
                        .WithMany("BinhLuans")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebTimTro.Data.BinhLuan", "BinhLuanCha")
                        .WithMany("BinhLuanCons")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("BinhLuans")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BinhLuanCha");

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTro", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "ChuTro")
                        .WithMany("PhongTros")
                        .HasForeignKey("ChuTroId");

                    b.Navigation("ChuTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroBooking", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "NguoiDung")
                        .WithMany("PhongTroBookings")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroBookings")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroDanhGia", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "NguoiDung")
                        .WithMany("PhongTroDanhGias")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroDanhGias")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroDichVu", b =>
                {
                    b.HasOne("WebTimTro.Data.DichVu", "DichVu")
                        .WithMany("PhongTroDichVus")
                        .HasForeignKey("DichVuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroDichVus")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DichVu");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroHinhAnh", b =>
                {
                    b.HasOne("WebTimTro.Data.HinhAnh", "HinhAnh")
                        .WithMany("PhongTroHinhAnhs")
                        .HasForeignKey("HinhAnhId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroHinhAnhs")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HinhAnh");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroLuuTru", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "NguoiDung")
                        .WithMany("PhongTroLuuTrus")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroLuuTrus")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroNote", b =>
                {
                    b.HasOne("WebTimTro.Data.Note", "Note")
                        .WithMany("PhongTroNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroNotes")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTroQuanTam", b =>
                {
                    b.HasOne("WebTimTro.Data.NguoiDung", "NguoiDung")
                        .WithMany("PhongTroQuanTams")
                        .HasForeignKey("NguoiDungId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTimTro.Data.PhongTro", "PhongTro")
                        .WithMany("PhongTroQuanTams")
                        .HasForeignKey("PhongTroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");

                    b.Navigation("PhongTro");
                });

            modelBuilder.Entity("WebTimTro.Data.BinhLuan", b =>
                {
                    b.Navigation("BinhLuanCons");
                });

            modelBuilder.Entity("WebTimTro.Data.DichVu", b =>
                {
                    b.Navigation("PhongTroDichVus");
                });

            modelBuilder.Entity("WebTimTro.Data.HinhAnh", b =>
                {
                    b.Navigation("PhongTroHinhAnhs");
                });

            modelBuilder.Entity("WebTimTro.Data.NguoiDung", b =>
                {
                    b.Navigation("BinhLuans");

                    b.Navigation("PhongTroBookings");

                    b.Navigation("PhongTroDanhGias");

                    b.Navigation("PhongTroLuuTrus");

                    b.Navigation("PhongTroQuanTams");

                    b.Navigation("PhongTros");
                });

            modelBuilder.Entity("WebTimTro.Data.Note", b =>
                {
                    b.Navigation("PhongTroNotes");
                });

            modelBuilder.Entity("WebTimTro.Data.PhongTro", b =>
                {
                    b.Navigation("BinhLuans");

                    b.Navigation("PhongTroBookings");

                    b.Navigation("PhongTroDanhGias");

                    b.Navigation("PhongTroDichVus");

                    b.Navigation("PhongTroHinhAnhs");

                    b.Navigation("PhongTroLuuTrus");

                    b.Navigation("PhongTroNotes");

                    b.Navigation("PhongTroQuanTams");
                });
#pragma warning restore 612, 618
        }
    }
}
