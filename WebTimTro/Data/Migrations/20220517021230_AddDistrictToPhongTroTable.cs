using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddDistrictToPhongTroTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "PhongTros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "8612987a-7568-4b55-9fc1-50b7f2fe2ce7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "33503d06-cf99-4242-96e0-614a62b68ba1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "5a6c8323-4ab0-4b2d-ae46-77452f5cfcf8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "PhongTros");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "432bfb7b-a53c-435a-aac2-346b6832e724");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "3fa9dada-d20a-40a7-8e5e-a80ab6571c9f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "76c3f5d4-4ddb-4ee9-b467-75672b9937ff");
        }
    }
}
