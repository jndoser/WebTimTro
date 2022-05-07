using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddRootIdIsReportedanIsShowColumnForBinhLuanTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "BinhLuans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "BinhLuans",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "RootId",
                table: "BinhLuans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "7663596f-5d47-497a-afca-6148820571b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "3c1111b4-4ef8-4077-8198-ba12b0dc30e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "2ff0e8e6-2c25-4800-8754-6b0df2556e0e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "BinhLuans");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "BinhLuans");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "BinhLuans");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "d7ce3257-dd7f-4701-ac42-4868affae4bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "3645a186-1531-43f3-96f3-51afd9d90fe9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "fe3f894e-06d0-4bd8-b638-dc5df8601d62");
        }
    }
}
