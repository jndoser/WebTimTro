using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class ChangeRootIdFromIntToLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RootId",
                table: "BinhLuans",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "2869eb5f-3f86-4df4-9932-d08bb189e2ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "4833d54d-5586-47e6-9c92-91166ad43711");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "6fb4ba46-6e03-4729-9c17-ad6af94085c0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RootId",
                table: "BinhLuans",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "4058ccf7-b149-4676-b149-3626521012ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "a42f27e3-3b8d-41c2-ad76-d0003e1681fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "a8b66680-34b5-4256-af02-afc25eb0d777");
        }
    }
}
