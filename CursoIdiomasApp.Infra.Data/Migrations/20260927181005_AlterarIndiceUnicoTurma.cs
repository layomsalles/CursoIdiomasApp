using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoIdiomasApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterarIndiceUnicoTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Turmas_Numero",
                table: "Turmas");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Numero_AnoLetivo",
                table: "Turmas",
                columns: new[] { "Numero", "AnoLetivo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Turmas_Numero_AnoLetivo",
                table: "Turmas");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_Numero",
                table: "Turmas",
                column: "Numero",
                unique: true);
        }
    }
}
