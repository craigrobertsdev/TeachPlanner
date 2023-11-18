using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Api.Database.Migrations
{
    /// <inheritdoc />
    public partial class TeacherCurruculumSubjectManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_curriculum_subjects_teachers_TeacherId",
                table: "curriculum_subjects");

            migrationBuilder.DropIndex(
                name: "IX_curriculum_subjects_TeacherId",
                table: "curriculum_subjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "curriculum_subjects");

            migrationBuilder.CreateTable(
                name: "CurriculumSubjectTeacher",
                columns: table => new
                {
                    SubjectsTaughtId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSubjectTeacher", x => new { x.SubjectsTaughtId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_CurriculumSubjectTeacher_curriculum_subjects_SubjectsTaughtId",
                        column: x => x.SubjectsTaughtId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumSubjectTeacher_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubjectTeacher_TeacherId",
                table: "CurriculumSubjectTeacher",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumSubjectTeacher");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "curriculum_subjects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_curriculum_subjects_TeacherId",
                table: "curriculum_subjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_curriculum_subjects_teachers_TeacherId",
                table: "curriculum_subjects",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");
        }
    }
}
