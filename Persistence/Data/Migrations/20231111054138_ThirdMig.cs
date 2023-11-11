using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignatura_grago_IdGradofk",
                table: "asignatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grago",
                table: "grago");

            migrationBuilder.RenameTable(
                name: "grago",
                newName: "grado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grado",
                table: "grado",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_asignatura_grado_IdGradofk",
                table: "asignatura",
                column: "IdGradofk",
                principalTable: "grado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignatura_grado_IdGradofk",
                table: "asignatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grado",
                table: "grado");

            migrationBuilder.RenameTable(
                name: "grado",
                newName: "grago");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grago",
                table: "grago",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_asignatura_grago_IdGradofk",
                table: "asignatura",
                column: "IdGradofk",
                principalTable: "grago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
