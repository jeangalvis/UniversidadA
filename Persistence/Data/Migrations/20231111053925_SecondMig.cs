using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignatura_profesor_IdProfesorfk",
                table: "asignatura");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "persona",
                type: "varchar(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 9)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_asignatura_profesor_IdProfesorfk",
                table: "asignatura",
                column: "IdProfesorfk",
                principalTable: "profesor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignatura_profesor_IdProfesorfk",
                table: "asignatura");

            migrationBuilder.UpdateData(
                table: "persona",
                keyColumn: "Telefono",
                keyValue: null,
                column: "Telefono",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "persona",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 9,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_asignatura_profesor_IdProfesorfk",
                table: "asignatura",
                column: "IdProfesorfk",
                principalTable: "profesor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
