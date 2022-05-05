using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddPhongTroQuanTamTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhongTroQuanTams",
                columns: table => new
                {
                    NguoiDungId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhongTroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongTroQuanTams", x => new { x.NguoiDungId, x.PhongTroId });
                    table.ForeignKey(
                        name: "FK_PhongTroQuanTams_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongTroQuanTams_PhongTros_PhongTroId",
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

            migrationBuilder.CreateIndex(
                name: "IX_PhongTroQuanTams_PhongTroId",
                table: "PhongTroQuanTams",
                column: "PhongTroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongTroQuanTams");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "12aff230-17b4-4487-a7be-a40efd763357");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "6b62411c-e663-4ead-809a-d4e5a8e72622");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "66c4d5f3-7777-43cc-8624-10596022f1bd");
        }
    }
}
