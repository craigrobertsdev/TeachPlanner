using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormativeAssessments",
                table: "FormativeAssessments");

            migrationBuilder.RenameTable(
                name: "FormativeAssessments",
                newName: "FormativeAssessment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormativeAssessment",
                table: "FormativeAssessment",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormativeAssessment",
                table: "FormativeAssessment");

            migrationBuilder.RenameTable(
                name: "FormativeAssessment",
                newName: "FormativeAssessments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormativeAssessments",
                table: "FormativeAssessments",
                column: "Id");
        }
    }
}
