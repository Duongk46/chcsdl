using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class DbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QLQA_BANAN",
                columns: table => new
                {
                    MaBanAn = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_BANAN", x => x.MaBanAn);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_LOAIMONAN",
                columns: table => new
                {
                    MaLoaiMonAn = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_LOAIMONAN", x => x.MaLoaiMonAn);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_LOAITAIKHOAN",
                columns: table => new
                {
                    MaLoaiTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_LOAITAIKHOAN", x => x.MaLoaiTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_TAIKHOAN",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiTaiKhoanID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_TAIKHOAN", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "FK_QLQA_TAIKHOAN_QLQA_LOAITAIKHOAN_LoaiTaiKhoanID",
                        column: x => x.LoaiTaiKhoanID,
                        principalTable: "QLQA_LOAITAIKHOAN",
                        principalColumn: "MaLoaiTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_HOADON",
                columns: table => new
                {
                    MaHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VAT = table.Column<float>(type: "real", nullable: false),
                    TongTien = table.Column<float>(type: "real", nullable: true),
                    BanAnID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_HOADON", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_QLQA_HOADON_QLQA_BANAN_BanAnID",
                        column: x => x.BanAnID,
                        principalTable: "QLQA_BANAN",
                        principalColumn: "MaBanAn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QLQA_HOADON_QLQA_TAIKHOAN_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "QLQA_TAIKHOAN",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_MONAN",
                columns: table => new
                {
                    MaMonAn = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMonAn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gia = table.Column<long>(type: "bigint", nullable: false),
                    LoaiMonAnID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_MONAN", x => x.MaMonAn);
                    table.ForeignKey(
                        name: "FK_QLQA_MONAN_QLQA_LOAIMONAN_LoaiMonAnID",
                        column: x => x.LoaiMonAnID,
                        principalTable: "QLQA_LOAIMONAN",
                        principalColumn: "MaLoaiMonAn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QLQA_MONAN_QLQA_TAIKHOAN_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "QLQA_TAIKHOAN",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QLQA_THONGTINHOADON",
                columns: table => new
                {
                    MaThongTinHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Tong = table.Column<float>(type: "real", nullable: false),
                    HoaDonID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MonAnID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QLQA_THONGTINHOADON", x => x.MaThongTinHoaDon);
                    table.ForeignKey(
                        name: "FK_QLQA_THONGTINHOADON_QLQA_HOADON_HoaDonID",
                        column: x => x.HoaDonID,
                        principalTable: "QLQA_HOADON",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QLQA_THONGTINHOADON_QLQA_MONAN_MonAnID",
                        column: x => x.MonAnID,
                        principalTable: "QLQA_MONAN",
                        principalColumn: "MaMonAn");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_HOADON_BanAnID",
                table: "QLQA_HOADON",
                column: "BanAnID");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_HOADON_CreatedBy",
                table: "QLQA_HOADON",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_MONAN_CreatedBy",
                table: "QLQA_MONAN",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_MONAN_LoaiMonAnID",
                table: "QLQA_MONAN",
                column: "LoaiMonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_TAIKHOAN_Email",
                table: "QLQA_TAIKHOAN",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_TAIKHOAN_LoaiTaiKhoanID",
                table: "QLQA_TAIKHOAN",
                column: "LoaiTaiKhoanID");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_TAIKHOAN_UserName",
                table: "QLQA_TAIKHOAN",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_THONGTINHOADON_HoaDonID",
                table: "QLQA_THONGTINHOADON",
                column: "HoaDonID");

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_THONGTINHOADON_MonAnID",
                table: "QLQA_THONGTINHOADON",
                column: "MonAnID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QLQA_THONGTINHOADON");

            migrationBuilder.DropTable(
                name: "QLQA_HOADON");

            migrationBuilder.DropTable(
                name: "QLQA_MONAN");

            migrationBuilder.DropTable(
                name: "QLQA_BANAN");

            migrationBuilder.DropTable(
                name: "QLQA_LOAIMONAN");

            migrationBuilder.DropTable(
                name: "QLQA_TAIKHOAN");

            migrationBuilder.DropTable(
                name: "QLQA_LOAITAIKHOAN");
        }
    }
}
