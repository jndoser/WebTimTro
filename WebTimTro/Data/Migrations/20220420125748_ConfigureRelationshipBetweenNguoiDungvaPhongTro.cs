using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class ConfigureRelationshipBetweenNguoiDungvaPhongTro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChuTroId",
                table: "PhongTros",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "6ab7946a-e0d1-4fb6-9332-04ebe12e90c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user",
                column: "ConcurrencyStamp",
                value: "e64878ab-6e96-410a-9e14-52dd7d9d26af");

            migrationBuilder.CreateIndex(
                name: "IX_PhongTros_ChuTroId",
                table: "PhongTros",
                column: "ChuTroId");

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

            migrationBuilder.DropIndex(
                name: "IX_PhongTros_ChuTroId",
                table: "PhongTros");

            migrationBuilder.DropColumn(
                name: "ChuTroId",
                table: "PhongTros");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "92745019-7be3-41c1-86ec-6cd7f889ac52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user",
                column: "ConcurrencyStamp",
                value: "5c87682b-6203-4950-976b-7f37bebcc2a2");
        }
    }
}
