using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_assessment_id_LessonPlans_LessonId",
                table: "lesson_assessment_id");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_resource_id_LessonPlans_LessonId",
                table: "lesson_resource_id");

            migrationBuilder.DropTable(
                name: "LessonComment");

            migrationBuilder.DropTable(
                name: "student_assessment_id");

            migrationBuilder.DropTable(
                name: "student_report_id");

            migrationBuilder.DropTable(
                name: "student_subject_id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonPlans",
                table: "LessonPlans");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "resources");

            migrationBuilder.RenameTable(
                name: "LessonPlans",
                newName: "lesson_plan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resources",
                table: "resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "lesson_comment",
                columns: table => new
                {
                    LessonCommentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StruckThrough = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_comment", x => x.LessonCommentId);
                    table.ForeignKey(
                        name: "FK_lesson_comment_lesson_plan_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_assessment_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_assessment_ids_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_report_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_report_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_report_ids_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_subject_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_subject_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_subject_ids_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_comment_LessonId",
                table: "lesson_comment",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_student_assessment_ids_StudentId",
                table: "student_assessment_ids",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_report_ids_StudentId",
                table: "student_report_ids",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_subject_ids_StudentId",
                table: "student_subject_ids",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_assessment_id_lesson_plan_LessonId",
                table: "lesson_assessment_id",
                column: "LessonId",
                principalTable: "lesson_plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_resource_id_lesson_plan_LessonId",
                table: "lesson_resource_id",
                column: "LessonId",
                principalTable: "lesson_plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_assessment_id_lesson_plan_LessonId",
                table: "lesson_assessment_id");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_resource_id_lesson_plan_LessonId",
                table: "lesson_resource_id");

            migrationBuilder.DropTable(
                name: "lesson_comment");

            migrationBuilder.DropTable(
                name: "student_assessment_ids");

            migrationBuilder.DropTable(
                name: "student_report_ids");

            migrationBuilder.DropTable(
                name: "student_subject_ids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resources",
                table: "resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan");

            migrationBuilder.RenameTable(
                name: "resources",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "lesson_plan",
                newName: "LessonPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonPlans",
                table: "LessonPlans",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LessonComment",
                columns: table => new
                {
                    LessonCommentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StruckThrough = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonComment", x => x.LessonCommentId);
                    table.ForeignKey(
                        name: "FK_LessonComment_LessonPlans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_assessment_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_assessment_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_assessment_id_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_report_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_report_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_report_id_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "student_subject_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_subject_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_subject_id_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LessonComment_LessonId",
                table: "LessonComment",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_student_assessment_id_StudentId",
                table: "student_assessment_id",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_report_id_StudentId",
                table: "student_report_id",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_subject_id_StudentId",
                table: "student_subject_id",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_assessment_id_LessonPlans_LessonId",
                table: "lesson_assessment_id",
                column: "LessonId",
                principalTable: "LessonPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_resource_id_LessonPlans_LessonId",
                table: "lesson_resource_id",
                column: "LessonId",
                principalTable: "LessonPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
