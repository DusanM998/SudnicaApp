using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudnica_API_Test.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePFLice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PravnoFizickoLice",
                table: "Kontakti",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PravnoFizickoLice",
                table: "Kontakti",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
