using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddNoActionOnDeleteBetweenPhongTroVaChuTro : Migration
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
                name: "FK_PhongTros_AspNetUsers_ChuTroId",
                table: "PhongTros",
                column: "ChuTroId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
