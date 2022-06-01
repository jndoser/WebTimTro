using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class RemoveNoActionOnDeleteOnPhongTroChuTro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhongTros_AspNetUsers_ChuTroId",
                table: "PhongTros");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "80c7e015-6b61-45dc-99b2-f78ab0d6ce36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "07f0e0e3-81c5-4ca3-8992-79d37d38b399");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "fe122e61-441d-4942-b394-9266c74f88ea");

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTros_AspNetUsers_ChuTroId",
                table: "PhongTros",
                column: "ChuTroId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhongTros_AspNetUsers_ChuTroId",
                table: "PhongTros");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "3c6e3397-5bb0-429e-8c7a-c2bd0a00a32c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "0dd61c29-e22e-4110-bb54-516cfbebc9d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "da109199-0a51-4088-be68-86b489dc37a9");

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTros_AspNetUsers_ChuTroId",
                table: "PhongTros",
                column: "ChuTroId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
