using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Agnello.CLI.Migrations
{
    /// <inheritdoc />
    public partial class AddBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "VINHOS");

            migrationBuilder.DropColumn(
                name: "Maker",
                table: "VINHOS");

            migrationBuilder.AddColumn<int>(
                name: "MakerId",
                table: "VINHOS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MARCAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARCAS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VINHOS_MakerId",
                table: "VINHOS",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_MARCAS_Name",
                table: "MARCAS",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_VINHOS_MARCAS_MakerId",
                table: "VINHOS",
                column: "MakerId",
                principalTable: "MARCAS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VINHOS_MARCAS_MakerId",
                table: "VINHOS");

            migrationBuilder.DropTable(
                name: "MARCAS");

            migrationBuilder.DropIndex(
                name: "IX_VINHOS_MakerId",
                table: "VINHOS");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "VINHOS");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "VINHOS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maker",
                table: "VINHOS",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
