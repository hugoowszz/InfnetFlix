using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfnetFlix.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    idPerfil = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nomePerfil = table.Column<string>(type: "TEXT", nullable: false),
                    idadePerfil = table.Column<int>(type: "INTEGER", nullable: false),
                    pin = table.Column<string>(type: "TEXT", nullable: true),
                    isInfantil = table.Column<bool>(type: "INTEGER", nullable: false),
                    emailLogado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.idPerfil);
                });

            migrationBuilder.CreateTable(
                name: "Titulos",
                columns: table => new
                {
                    idTitulo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nomeTitulo = table.Column<string>(type: "TEXT", nullable: false),
                    idadeMinima = table.Column<int>(type: "INTEGER", nullable: false),
                    imagemCapa = table.Column<string>(type: "TEXT", nullable: false),
                    duracao = table.Column<int>(type: "INTEGER", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: false),
                    categoria = table.Column<string>(type: "TEXT", nullable: false),
                    qualidade = table.Column<string>(type: "TEXT", nullable: false),
                    conteudo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulos", x => x.idTitulo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nomeUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    idadeUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    emailUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    senhaUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    isPremium = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Titulos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
