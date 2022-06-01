using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddCascadeBetweenBinhluanVaNguoiDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuans_AspNetUsers_NguoiDungId",
                table: "BinhLuans");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "5407d5ac-fbdb-4b51-bdba-0a4ce4e36402");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "542faae2-3101-4fb0-8972-391daa892a19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "2c86314f-b1ed-4369-8b01-1b0bc52e83cb");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuans_AspNetUsers_NguoiDungId",
                table: "BinhLuans",
                column: "NguoiDungId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuans_AspNetUsers_NguoiDungId",
                table: "BinhLuans");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "4745537d-da2d-451f-aa5c-d96ecde915f3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "8eb7a9fd-8634-41a7-b946-b6ce606943db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "8b1e1b95-8987-429a-976e-af8561c21fb4");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuans_AspNetUsers_NguoiDungId",
                table: "BinhLuans",
                column: "NguoiDungId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
