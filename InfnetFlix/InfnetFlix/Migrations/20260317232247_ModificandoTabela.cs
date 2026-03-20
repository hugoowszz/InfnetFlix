using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetFlix.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Progressos",
                table: "Progressos");

            migrationBuilder.AlterColumn<int>(
                name: "idPerfil",
                table: "Progressos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progressos",
                table: "Progressos",
                columns: new[] { "idPerfil", "idTitulo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Progressos",
                table: "Progressos");

            migrationBuilder.AlterColumn<int>(
                name: "idPerfil",
                table: "Progressos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Progressos",
                table: "Progressos",
                column: "idPerfil");
        }
    }
}
