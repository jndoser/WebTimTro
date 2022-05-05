using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddPhongTroLuuTruTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhongTroLuuTrus",
                columns: table => new
                {
                    NguoiDungId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhongTroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongTroLuuTrus", x => new { x.NguoiDungId, x.PhongTroId });
                    table.ForeignKey(
                        name: "FK_PhongTroLuuTrus_AspNetUsers_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhongTroLuuTrus_PhongTros_PhongTroId",
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

            migrationBuilder.CreateIndex(
                name: "IX_PhongTroLuuTrus_PhongTroId",
                table: "PhongTroLuuTrus",
                column: "PhongTroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhongTroLuuTrus");

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
        }
    }
}
