using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academico.Migrations
{
    /// <inheritdoc />
    public partial class avaliacaoalunoavaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    AvaliacaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.AvaliacaoID);
                });

            migrationBuilder.CreateTable(
                name: "AlunosAvaliacoes",
                columns: table => new
                {
                    AlunoID = table.Column<int>(type: "int", nullable: false),
                    AvaliacaoID = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosAvaliacoes", x => new { x.AlunoID, x.AvaliacaoID });
                    table.ForeignKey(
                        name: "FK_AlunosAvaliacoes_Alunos_AlunoID",
                        column: x => x.AlunoID,
                        principalTable: "Alunos",
                        principalColumn: "AlunoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosAvaliacoes_Avaliacoes_AvaliacaoID",
                        column: x => x.AvaliacaoID,
                        principalTable: "Avaliacoes",
                        principalColumn: "AvaliacaoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosAvaliacoes_AvaliacaoID",
                table: "AlunosAvaliacoes",
                column: "AvaliacaoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosAvaliacoes");

            migrationBuilder.DropTable(
                name: "Avaliacoes");
        }
    }
}
