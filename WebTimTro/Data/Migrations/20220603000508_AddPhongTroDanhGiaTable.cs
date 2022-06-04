using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddPhongTroDanhGiaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhongTroDanhGias",
                columns: table => new
                {
                    NguoiDungId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhongTroId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongTroDanhGias", x => new { x.NguoiDungId, x.PhongTroId });
                    table.ForeignKey(
                        name: "FK_PhongTroDanhGias_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongTroDanhGias_PhongTros_PhongTroId",
                        column: x => x.PhongTroId,
                        principalTable: "PhongTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "4ccecb8c-d246-40f4-8767-429606013728");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "77a42cb3-04dc-4489-b91a-31ad640544a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "31ad5b44-5a94-47f4-9d13-ffde94e384ff");

            migrationBuilder.CreateIndex(
                name: "IX_PhongTroDanhGias_PhongTroId",
                table: "PhongTroDanhGias",
                column: "PhongTroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongTroDanhGias");

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
        }
    }
}
