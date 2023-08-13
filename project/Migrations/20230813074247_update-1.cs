using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QLQA_MONAN_QLQA_TAIKHOAN_CreatedBy",
                table: "QLQA_MONAN");

            migrationBuilder.DropForeignKey(
                name: "FK_QLQA_THONGTINHOADON_QLQA_MONAN_MonAnID",
                table: "QLQA_THONGTINHOADON");

            migrationBuilder.DropIndex(
                name: "IX_QLQA_MONAN_CreatedBy",
                table: "QLQA_MONAN");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "QLQA_MONAN");

            migrationBuilder.AddForeignKey(
                name: "FK_QLQA_THONGTINHOADON_QLQA_MONAN_MonAnID",
                table: "QLQA_THONGTINHOADON",
                column: "MonAnID",
                principalTable: "QLQA_MONAN",
                principalColumn: "MaMonAn",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QLQA_THONGTINHOADON_QLQA_MONAN_MonAnID",
                table: "QLQA_THONGTINHOADON");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "QLQA_MONAN",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QLQA_MONAN_CreatedBy",
                table: "QLQA_MONAN",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_QLQA_MONAN_QLQA_TAIKHOAN_CreatedBy",
                table: "QLQA_MONAN",
                column: "CreatedBy",
                principalTable: "QLQA_TAIKHOAN",
                principalColumn: "MaTaiKhoan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QLQA_THONGTINHOADON_QLQA_MONAN_MonAnID",
                table: "QLQA_THONGTINHOADON",
                column: "MonAnID",
                principalTable: "QLQA_MONAN",
                principalColumn: "MaMonAn");
        }
    }
}
