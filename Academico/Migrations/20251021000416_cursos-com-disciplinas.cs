using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academico.Migrations
{
    /// <inheritdoc />
    public partial class cursoscomdisciplinas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CursoDisciplina",
                columns: table => new
                {
                    CursosCursoID = table.Column<int>(type: "int", nullable: false),
                    DisciplinasDisciplinaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoDisciplina", x => new { x.CursosCursoID, x.DisciplinasDisciplinaID });
                    table.ForeignKey(
                        name: "FK_CursoDisciplina_Cursos_CursosCursoID",
                        column: x => x.CursosCursoID,
                        principalTable: "Cursos",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoDisciplina_Disciplinas_DisciplinasDisciplinaID",
                        column: x => x.DisciplinasDisciplinaID,
                        principalTable: "Disciplinas",
                        principalColumn: "DisciplinaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoDisciplina_DisciplinasDisciplinaID",
                table: "CursoDisciplina",
                column: "DisciplinasDisciplinaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoDisciplina");
        }
    }
}
