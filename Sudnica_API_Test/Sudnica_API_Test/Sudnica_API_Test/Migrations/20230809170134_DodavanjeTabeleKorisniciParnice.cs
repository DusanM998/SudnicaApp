using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudnica_API_Test.Migrations
{
    /// <inheritdoc />
    public partial class DodavanjeTabeleKorisniciParnice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KorisniciParnice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParnicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciParnice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisniciParnice_AspNetUsers_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisniciParnice_Parnice_ParnicaId",
                        column: x => x.ParnicaId,
                        principalTable: "Parnice",
                        principalColumn: "ParnicaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciParnice_KorisnikId",
                table: "KorisniciParnice",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciParnice_ParnicaId",
                table: "KorisniciParnice",
                column: "ParnicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciParnice");
        }
    }
}
