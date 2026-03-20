using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetFlix.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelaProgressos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progressos",
                columns: table => new
                {
                    idPerfil = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idTitulo = table.Column<int>(type: "INTEGER", nullable: false),
                    minutoParada = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressos", x => x.idPerfil);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Progressos");
        }
    }
}
