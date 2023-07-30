using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormativeAssessment",
                table: "FormativeAssessment");

            migrationBuilder.RenameTable(
                name: "FormativeAssessment",
                newName: "formative_assessment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_formative_assessment",
                table: "formative_assessment",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_formative_assessment",
                table: "formative_assessment");

            migrationBuilder.RenameTable(
                name: "formative_assessment",
                newName: "FormativeAssessment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormativeAssessment",
                table: "FormativeAssessment",
                column: "Id");
        }
    }
}
