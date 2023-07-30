using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptor_Strand_Strand",
                table: "content_descriptor");

            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptor_Substrand_Substrand",
                table: "content_descriptor");

            migrationBuilder.DropForeignKey(
                name: "FK_Elaboration_content_descriptor_ContentDescriptor",
                table: "Elaboration");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_comment_lesson_plan_LessonId",
                table: "lesson_comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Strand_year_level_YearLevel",
                table: "Strand");

            migrationBuilder.DropForeignKey(
                name: "FK_Substrand_Strand_Strand",
                table: "Substrand");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessment_summative_assessment_result_ResultId",
                table: "summative_assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_year_level_Subjects_Subject",
                table: "year_level");

            migrationBuilder.DropTable(
                name: "lesson_assessment_id");

            migrationBuilder.DropTable(
                name: "lesson_resource_id");

            migrationBuilder.DropTable(
                name: "report_comment");

            migrationBuilder.DropTable(
                name: "teacher_assessment_id");

            migrationBuilder.DropTable(
                name: "teacher_report_id");

            migrationBuilder.DropTable(
                name: "teacher_resource_id");

            migrationBuilder.DropTable(
                name: "teacher_student_id");

            migrationBuilder.DropTable(
                name: "teacher_subject_id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_year_level",
                table: "year_level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment_result",
                table: "summative_assessment_result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment",
                table: "summative_assessment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Substrand",
                table: "Substrand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Strand",
                table: "Strand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_report",
                table: "report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_formative_assessment",
                table: "formative_assessment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Elaboration",
                table: "Elaboration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_content_descriptor",
                table: "content_descriptor");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "teachers");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "subjects");

            migrationBuilder.RenameTable(
                name: "year_level",
                newName: "year_levels");

            migrationBuilder.RenameTable(
                name: "summative_assessment_result",
                newName: "summative_assessment_results");

            migrationBuilder.RenameTable(
                name: "summative_assessment",
                newName: "summative_assessments");

            migrationBuilder.RenameTable(
                name: "Substrand",
                newName: "substrands");

            migrationBuilder.RenameTable(
                name: "Strand",
                newName: "strands");

            migrationBuilder.RenameTable(
                name: "report",
                newName: "reports");

            migrationBuilder.RenameTable(
                name: "lesson_plan",
                newName: "lesson_plans");

            migrationBuilder.RenameTable(
                name: "formative_assessment",
                newName: "formative_assessments");

            migrationBuilder.RenameTable(
                name: "Elaboration",
                newName: "elaborations");

            migrationBuilder.RenameTable(
                name: "content_descriptor",
                newName: "content_descriptors");

            migrationBuilder.RenameIndex(
                name: "IX_year_level_Subject",
                table: "year_levels",
                newName: "IX_year_levels_Subject");

            migrationBuilder.RenameIndex(
                name: "IX_summative_assessment_ResultId",
                table: "summative_assessments",
                newName: "IX_summative_assessments_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Substrand_Strand",
                table: "substrands",
                newName: "IX_substrands_Strand");

            migrationBuilder.RenameIndex(
                name: "IX_Strand_YearLevel",
                table: "strands",
                newName: "IX_strands_YearLevel");

            migrationBuilder.RenameIndex(
                name: "IX_Elaboration_ContentDescriptor",
                table: "elaborations",
                newName: "IX_elaborations_ContentDescriptor");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptor_Substrand",
                table: "content_descriptors",
                newName: "IX_content_descriptors_Substrand");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptor_Strand",
                table: "content_descriptors",
                newName: "IX_content_descriptors_Strand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                table: "teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjects",
                table: "subjects",
                column: "SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_year_levels",
                table: "year_levels",
                column: "YearLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results",
                column: "SummativeAssessmentResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessments",
                table: "summative_assessments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_substrands",
                table: "substrands",
                column: "SubstrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_strands",
                table: "strands",
                column: "StrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reports",
                table: "reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_plans",
                table: "lesson_plans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_formative_assessments",
                table: "formative_assessments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_elaborations",
                table: "elaborations",
                column: "Elaboration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_content_descriptors",
                table: "content_descriptors",
                column: "ContentDescriptorId");

            migrationBuilder.CreateTable(
                name: "lesson_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_assessment_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_assessment_ids_lesson_plans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_resource_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_resource_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_resource_ids_lesson_plans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report_comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Grade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CharacterLimit = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_report_comments_reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherAssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_assessment_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_assessment_ids_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_report_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_report_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_report_ids_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_resource_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_resource_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_resource_ids_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_student_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherStudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_student_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_student_ids_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_subject_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_subject_ids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_subject_ids_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_assessment_ids_LessonId",
                table: "lesson_assessment_ids",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_resource_ids_LessonId",
                table: "lesson_resource_ids",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_report_comments_ReportId",
                table: "report_comments",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_assessment_ids_TeacherId",
                table: "teacher_assessment_ids",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_report_ids_TeacherId",
                table: "teacher_report_ids",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_resource_ids_TeacherId",
                table: "teacher_resource_ids",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_student_ids_TeacherId",
                table: "teacher_student_ids",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_subject_ids_TeacherId",
                table: "teacher_subject_ids",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptors_strands_Strand",
                table: "content_descriptors",
                column: "Strand",
                principalTable: "strands",
                principalColumn: "StrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptors_substrands_Substrand",
                table: "content_descriptors",
                column: "Substrand",
                principalTable: "substrands",
                principalColumn: "SubstrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptor",
                table: "elaborations",
                column: "ContentDescriptor",
                principalTable: "content_descriptors",
                principalColumn: "ContentDescriptorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_comment_lesson_plans_LessonId",
                table: "lesson_comment",
                column: "LessonId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_strands_year_levels_YearLevel",
                table: "strands",
                column: "YearLevel",
                principalTable: "year_levels",
                principalColumn: "YearLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_substrands_strands_Strand",
                table: "substrands",
                column: "Strand",
                principalTable: "strands",
                principalColumn: "StrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessments_summative_assessment_results_ResultId",
                table: "summative_assessments",
                column: "ResultId",
                principalTable: "summative_assessment_results",
                principalColumn: "SummativeAssessmentResultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_year_levels_subjects_Subject",
                table: "year_levels",
                column: "Subject",
                principalTable: "subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptors_strands_Strand",
                table: "content_descriptors");

            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptors_substrands_Substrand",
                table: "content_descriptors");

            migrationBuilder.DropForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptor",
                table: "elaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_comment_lesson_plans_LessonId",
                table: "lesson_comment");

            migrationBuilder.DropForeignKey(
                name: "FK_strands_year_levels_YearLevel",
                table: "strands");

            migrationBuilder.DropForeignKey(
                name: "FK_substrands_strands_Strand",
                table: "substrands");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessments_summative_assessment_results_ResultId",
                table: "summative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_year_levels_subjects_Subject",
                table: "year_levels");

            migrationBuilder.DropTable(
                name: "lesson_assessment_ids");

            migrationBuilder.DropTable(
                name: "lesson_resource_ids");

            migrationBuilder.DropTable(
                name: "report_comments");

            migrationBuilder.DropTable(
                name: "teacher_assessment_ids");

            migrationBuilder.DropTable(
                name: "teacher_report_ids");

            migrationBuilder.DropTable(
                name: "teacher_resource_ids");

            migrationBuilder.DropTable(
                name: "teacher_student_ids");

            migrationBuilder.DropTable(
                name: "teacher_subject_ids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subjects",
                table: "subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_year_levels",
                table: "year_levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessments",
                table: "summative_assessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_substrands",
                table: "substrands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_strands",
                table: "strands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reports",
                table: "reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_plans",
                table: "lesson_plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_formative_assessments",
                table: "formative_assessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_elaborations",
                table: "elaborations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_content_descriptors",
                table: "content_descriptors");

            migrationBuilder.RenameTable(
                name: "teachers",
                newName: "Teachers");

            migrationBuilder.RenameTable(
                name: "subjects",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "year_levels",
                newName: "year_level");

            migrationBuilder.RenameTable(
                name: "summative_assessments",
                newName: "summative_assessment");

            migrationBuilder.RenameTable(
                name: "summative_assessment_results",
                newName: "summative_assessment_result");

            migrationBuilder.RenameTable(
                name: "substrands",
                newName: "Substrand");

            migrationBuilder.RenameTable(
                name: "strands",
                newName: "Strand");

            migrationBuilder.RenameTable(
                name: "reports",
                newName: "report");

            migrationBuilder.RenameTable(
                name: "lesson_plans",
                newName: "lesson_plan");

            migrationBuilder.RenameTable(
                name: "formative_assessments",
                newName: "formative_assessment");

            migrationBuilder.RenameTable(
                name: "elaborations",
                newName: "Elaboration");

            migrationBuilder.RenameTable(
                name: "content_descriptors",
                newName: "content_descriptor");

            migrationBuilder.RenameIndex(
                name: "IX_year_levels_Subject",
                table: "year_level",
                newName: "IX_year_level_Subject");

            migrationBuilder.RenameIndex(
                name: "IX_summative_assessments_ResultId",
                table: "summative_assessment",
                newName: "IX_summative_assessment_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_substrands_Strand",
                table: "Substrand",
                newName: "IX_Substrand_Strand");

            migrationBuilder.RenameIndex(
                name: "IX_strands_YearLevel",
                table: "Strand",
                newName: "IX_Strand_YearLevel");

            migrationBuilder.RenameIndex(
                name: "IX_elaborations_ContentDescriptor",
                table: "Elaboration",
                newName: "IX_Elaboration_ContentDescriptor");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptors_Substrand",
                table: "content_descriptor",
                newName: "IX_content_descriptor_Substrand");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptors_Strand",
                table: "content_descriptor",
                newName: "IX_content_descriptor_Strand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_year_level",
                table: "year_level",
                column: "YearLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment",
                table: "summative_assessment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment_result",
                table: "summative_assessment_result",
                column: "SummativeAssessmentResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Substrand",
                table: "Substrand",
                column: "SubstrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Strand",
                table: "Strand",
                column: "StrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_report",
                table: "report",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_plan",
                table: "lesson_plan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_formative_assessment",
                table: "formative_assessment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Elaboration",
                table: "Elaboration",
                column: "Elaboration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_content_descriptor",
                table: "content_descriptor",
                column: "ContentDescriptorId");

            migrationBuilder.CreateTable(
                name: "lesson_assessment_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_assessment_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_assessment_id_lesson_plan_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_resource_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_resource_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_resource_id_lesson_plan_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lesson_plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report_comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CharacterLimit = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_report_comment_report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_assessment_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherAssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_assessment_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_assessment_id_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_report_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_report_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_report_id_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_resource_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_resource_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_resource_id_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_student_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherStudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_student_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_student_id_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teacher_subject_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_subject_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teacher_subject_id_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_assessment_id_LessonId",
                table: "lesson_assessment_id",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_resource_id_LessonId",
                table: "lesson_resource_id",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_report_comment_ReportId",
                table: "report_comment",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_assessment_id_TeacherId",
                table: "teacher_assessment_id",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_report_id_TeacherId",
                table: "teacher_report_id",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_resource_id_TeacherId",
                table: "teacher_resource_id",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_student_id_TeacherId",
                table: "teacher_student_id",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_subject_id_TeacherId",
                table: "teacher_subject_id",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptor_Strand_Strand",
                table: "content_descriptor",
                column: "Strand",
                principalTable: "Strand",
                principalColumn: "StrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptor_Substrand_Substrand",
                table: "content_descriptor",
                column: "Substrand",
                principalTable: "Substrand",
                principalColumn: "SubstrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Elaboration_content_descriptor_ContentDescriptor",
                table: "Elaboration",
                column: "ContentDescriptor",
                principalTable: "content_descriptor",
                principalColumn: "ContentDescriptorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_comment_lesson_plan_LessonId",
                table: "lesson_comment",
                column: "LessonId",
                principalTable: "lesson_plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Strand_year_level_YearLevel",
                table: "Strand",
                column: "YearLevel",
                principalTable: "year_level",
                principalColumn: "YearLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Substrand_Strand_Strand",
                table: "Substrand",
                column: "Strand",
                principalTable: "Strand",
                principalColumn: "StrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessment_summative_assessment_result_ResultId",
                table: "summative_assessment",
                column: "ResultId",
                principalTable: "summative_assessment_result",
                principalColumn: "SummativeAssessmentResultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_year_level_Subjects_Subject",
                table: "year_level",
                column: "Subject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
