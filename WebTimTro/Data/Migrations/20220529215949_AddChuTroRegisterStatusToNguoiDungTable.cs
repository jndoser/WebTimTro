using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddChuTroRegisterStatusToNguoiDungTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChuTroRegisterStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChuTroRegisterStatus",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "9301b2b8-bd3d-4e31-abbf-45a3102c4656");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "818b6e2e-4994-433a-80b7-fb02af0eb266");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "956de17e-3267-47fa-be84-0ec1ff381184");
        }
    }
}
