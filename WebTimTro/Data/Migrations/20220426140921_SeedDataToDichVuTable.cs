using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class SeedDataToDichVuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "8a21652a-dabe-42b1-8992-63184a8bef1a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "ddceeb72-67e3-46d0-b8ab-a052d8f54006");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "d3dc4293-ee3f-4e0a-9d9d-549d16fe3ac0");

            migrationBuilder.InsertData(
                table: "DichVus",
                columns: new[] { "Id", "Icon", "Ten" },
                values: new object[,]
                {
                    { 1, null, "Wifi" },
                    { 2, null, "Máy giặc" },
                    { 3, null, "Máy lạnh" },
                    { 4, null, "Tủ lạnh" },
                    { 5, null, "TV với truyền hình cáp tiêu chuẩn" },
                    { 6, null, "Máy sấy quần áo" },
                    { 7, null, "Camera an ninh trong nhà" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "9965c95d-66ae-4b57-9dfd-7e74fdfb7ded");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "cb038e07-a5eb-4764-8722-2ab066150011");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "0d62000f-c8be-493c-9eca-efcc5f9b84c9");
        }
    }
}
