﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project.Entities;

#nullable disable

namespace project.Migrations
{
    [DbContext(typeof(QuanAnContext))]
    [Migration("20230813074247_update-1")]
    partial class update1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("project.Entities.BanAn", b =>
                {
                    b.Property<Guid>("MaBanAn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaBanAn");

                    b.ToTable("QLQA_BANAN");
                });

            modelBuilder.Entity("project.Entities.HoaDon", b =>
                {
                    b.Property<Guid>("MaHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BanAnID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayUpdate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<float?>("TongTien")
                        .HasColumnType("real");

                    b.Property<float>("VAT")
                        .HasColumnType("real");

                    b.HasKey("MaHoaDon");

                    b.HasIndex("BanAnID");

                    b.HasIndex("CreatedBy");

                    b.ToTable("QLQA_HOADON");
                });

            modelBuilder.Entity("project.Entities.LoaiMonAn", b =>
                {
                    b.Property<Guid>("MaLoaiMonAn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaLoaiMonAn");

                    b.ToTable("QLQA_LOAIMONAN");
                });

            modelBuilder.Entity("project.Entities.LoaiTaiKhoan", b =>
                {
                    b.Property<Guid>("MaLoaiTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayUpdate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaLoaiTaiKhoan");

                    b.ToTable("QLQA_LOAITAIKHOAN");
                });

            modelBuilder.Entity("project.Entities.MonAn", b =>
                {
                    b.Property<Guid>("MaMonAn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Gia")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LoaiMonAnID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayUpdate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TenMonAn")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MaMonAn");

                    b.HasIndex("LoaiMonAnID");

                    b.ToTable("QLQA_MONAN");
                });

            modelBuilder.Entity("project.Entities.TaiKhoan", b =>
                {
                    b.Property<Guid>("MaTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GioiTinh")
                        .HasColumnType("int");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("LoaiTaiKhoanID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaTaiKhoan");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LoaiTaiKhoanID");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("QLQA_TAIKHOAN");
                });

            modelBuilder.Entity("project.Entities.ThongTinHoaHon", b =>
                {
                    b.Property<Guid?>("MaThongTinHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("HoaDonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MonAnID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<float>("Tong")
                        .HasColumnType("real");

                    b.HasKey("MaThongTinHoaDon");

                    b.HasIndex("HoaDonID");

                    b.HasIndex("MonAnID");

                    b.ToTable("QLQA_THONGTINHOADON");
                });

            modelBuilder.Entity("project.Entities.HoaDon", b =>
                {
                    b.HasOne("project.Entities.BanAn", "BanAn")
                        .WithMany("HoaDons")
                        .HasForeignKey("BanAnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project.Entities.TaiKhoan", "TaiKhoan")
                        .WithMany("HoaDons")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BanAn");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("project.Entities.MonAn", b =>
                {
                    b.HasOne("project.Entities.LoaiMonAn", "LoaiMonAn")
                        .WithMany("MonAns")
                        .HasForeignKey("LoaiMonAnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiMonAn");
                });

            modelBuilder.Entity("project.Entities.TaiKhoan", b =>
                {
                    b.HasOne("project.Entities.LoaiTaiKhoan", "LoaiTaiKhoan")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("LoaiTaiKhoanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiTaiKhoan");
                });

            modelBuilder.Entity("project.Entities.ThongTinHoaHon", b =>
                {
                    b.HasOne("project.Entities.HoaDon", "HoaDon")
                        .WithMany("ThongTinHoaHons")
                        .HasForeignKey("HoaDonID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("project.Entities.MonAn", "MonAn")
                        .WithMany("ThongTinHoaHons")
                        .HasForeignKey("MonAnID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("HoaDon");

                    b.Navigation("MonAn");
                });

            modelBuilder.Entity("project.Entities.BanAn", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("project.Entities.HoaDon", b =>
                {
                    b.Navigation("ThongTinHoaHons");
                });

            modelBuilder.Entity("project.Entities.LoaiMonAn", b =>
                {
                    b.Navigation("MonAns");
                });

            modelBuilder.Entity("project.Entities.LoaiTaiKhoan", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("project.Entities.MonAn", b =>
                {
                    b.Navigation("ThongTinHoaHons");
                });

            modelBuilder.Entity("project.Entities.TaiKhoan", b =>
                {
                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
