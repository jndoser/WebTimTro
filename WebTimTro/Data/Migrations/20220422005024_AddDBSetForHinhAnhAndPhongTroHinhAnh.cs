using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTimTro.Data.Migrations
{
    public partial class AddDBSetForHinhAnhAndPhongTroHinhAnh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhongTroHinhAnh_HinhAnh_HinhAnhId",
                table: "PhongTroHinhAnh");

            migrationBuilder.DropForeignKey(
                name: "FK_PhongTroHinhAnh_PhongTros_PhongTroId",
                table: "PhongTroHinhAnh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhongTroHinhAnh",
                table: "PhongTroHinhAnh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HinhAnh",
                table: "HinhAnh");

            migrationBuilder.RenameTable(
                name: "PhongTroHinhAnh",
                newName: "PhongTroHinhAnhs");

            migrationBuilder.RenameTable(
                name: "HinhAnh",
                newName: "HinhAnhs");

            migrationBuilder.RenameIndex(
                name: "IX_PhongTroHinhAnh_HinhAnhId",
                table: "PhongTroHinhAnhs",
                newName: "IX_PhongTroHinhAnhs_HinhAnhId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhongTroHinhAnhs",
                table: "PhongTroHinhAnhs",
                columns: new[] { "PhongTroId", "HinhAnhId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HinhAnhs",
                table: "HinhAnhs",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTroHinhAnhs_HinhAnhs_HinhAnhId",
                table: "PhongTroHinhAnhs",
                column: "HinhAnhId",
                principalTable: "HinhAnhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTroHinhAnhs_PhongTros_PhongTroId",
                table: "PhongTroHinhAnhs",
                column: "PhongTroId",
                principalTable: "PhongTros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhongTroHinhAnhs_HinhAnhs_HinhAnhId",
                table: "PhongTroHinhAnhs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhongTroHinhAnhs_PhongTros_PhongTroId",
                table: "PhongTroHinhAnhs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhongTroHinhAnhs",
                table: "PhongTroHinhAnhs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HinhAnhs",
                table: "HinhAnhs");

            migrationBuilder.RenameTable(
                name: "PhongTroHinhAnhs",
                newName: "PhongTroHinhAnh");

            migrationBuilder.RenameTable(
                name: "HinhAnhs",
                newName: "HinhAnh");

            migrationBuilder.RenameIndex(
                name: "IX_PhongTroHinhAnhs_HinhAnhId",
                table: "PhongTroHinhAnh",
                newName: "IX_PhongTroHinhAnh_HinhAnhId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhongTroHinhAnh",
                table: "PhongTroHinhAnh",
                columns: new[] { "PhongTroId", "HinhAnhId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HinhAnh",
                table: "HinhAnh",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin",
                column: "ConcurrencyStamp",
                value: "5e18a70c-a289-4112-84bb-8dee254daf97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "chutro",
                column: "ConcurrencyStamp",
                value: "71bbdacc-6fc4-4129-8ca9-81a834bc1b73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "nguoidung",
                column: "ConcurrencyStamp",
                value: "25a50476-2c3f-460d-b96c-c46cb4a1cb2d");

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTroHinhAnh_HinhAnh_HinhAnhId",
                table: "PhongTroHinhAnh",
                column: "HinhAnhId",
                principalTable: "HinhAnh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhongTroHinhAnh_PhongTros_PhongTroId",
                table: "PhongTroHinhAnh",
                column: "PhongTroId",
                principalTable: "PhongTros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
