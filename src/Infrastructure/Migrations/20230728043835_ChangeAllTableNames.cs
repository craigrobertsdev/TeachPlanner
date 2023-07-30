using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAllTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentDescriptor_Strand_Strand",
                table: "ContentDescriptor");

            migrationBuilder.DropForeignKey(
                name: "FK_ContentDescriptor_Substrand_Substrand",
                table: "ContentDescriptor");

            migrationBuilder.DropForeignKey(
                name: "FK_Elaboration_ContentDescriptor_ContentDescriptor",
                table: "Elaboration");

            migrationBuilder.DropForeignKey(
                name: "FK_Strand_YearLevel_YearLevel",
                table: "Strand");

            migrationBuilder.DropForeignKey(
                name: "FK_SummativeAssessments_SummativeAssessmentResult_ResultId",
                table: "SummativeAssessments");

            migrationBuilder.DropForeignKey(
                name: "FK_TermPlan_YearPlanners_YearPlannerId",
                table: "TermPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_YearLevel_Subjects_Subject",
                table: "YearLevel");

            migrationBuilder.DropTable(
                name: "LessonAssessmentId");

            migrationBuilder.DropTable(
                name: "LessonResourceId");

            migrationBuilder.DropTable(
                name: "ReportComment");

            migrationBuilder.DropTable(
                name: "StudentAssessmentId");

            migrationBuilder.DropTable(
                name: "StudentReportId");

            migrationBuilder.DropTable(
                name: "StudentSubjectId");

            migrationBuilder.DropTable(
                name: "TeacherAssessmentId");

            migrationBuilder.DropTable(
                name: "TeacherReportId");

            migrationBuilder.DropTable(
                name: "TeacherResourceId");

            migrationBuilder.DropTable(
                name: "TeacherStudentId");

            migrationBuilder.DropTable(
                name: "TeacherSubjectId");

            migrationBuilder.DropTable(
                name: "TermPlannerSchoolEventId");

            migrationBuilder.DropTable(
                name: "TermPlannerWeekPlannerId");

            migrationBuilder.DropTable(
                name: "WeekPlannerLessonPlanId");

            migrationBuilder.DropTable(
                name: "WeekPlannerSchoolEventId");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YearPlanners",
                table: "YearPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YearLevel",
                table: "YearLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeekPlanners",
                table: "WeekPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TermPlanners",
                table: "TermPlanners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummativeAssessments",
                table: "SummativeAssessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummativeAssessmentResult",
                table: "SummativeAssessmentResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentDescriptor",
                table: "ContentDescriptor");

            migrationBuilder.RenameTable(
                name: "YearPlanners",
                newName: "term_plan");

            migrationBuilder.RenameTable(
                name: "YearLevel",
                newName: "year_level");

            migrationBuilder.RenameTable(
                name: "WeekPlanners",
                newName: "week_planner");

            migrationBuilder.RenameTable(
                name: "TermPlanners",
                newName: "term_planner");

            migrationBuilder.RenameTable(
                name: "SummativeAssessments",
                newName: "summative_assessment");

            migrationBuilder.RenameTable(
                name: "SummativeAssessmentResult",
                newName: "summative_assessment_result");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "report");

            migrationBuilder.RenameTable(
                name: "ContentDescriptor",
                newName: "content_descriptor");

            migrationBuilder.RenameIndex(
                name: "IX_YearLevel_Subject",
                table: "year_level",
                newName: "IX_year_level_Subject");

            migrationBuilder.RenameIndex(
                name: "IX_SummativeAssessments_ResultId",
                table: "summative_assessment",
                newName: "IX_summative_assessment_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_ContentDescriptor_Substrand",
                table: "content_descriptor",
                newName: "IX_content_descriptor_Substrand");

            migrationBuilder.RenameIndex(
                name: "IX_ContentDescriptor_Strand",
                table: "content_descriptor",
                newName: "IX_content_descriptor_Strand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_term_plan",
                table: "term_plan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_year_level",
                table: "year_level",
                column: "YearLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_week_planner",
                table: "week_planner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_term_planner",
                table: "term_planner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment",
                table: "summative_assessment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment_result",
                table: "summative_assessment_result",
                column: "SummativeAssessmentResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_report",
                table: "report",
                column: "Id");

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
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_assessment_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_assessment_id_LessonPlans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonPlans",
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
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_resource_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_resource_id_LessonPlans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report_comment",
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
                name: "student_assessment_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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

            migrationBuilder.CreateTable(
                name: "teacher_assessment_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherAssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherStudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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

            migrationBuilder.CreateTable(
                name: "term_planner_school_event_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_planner_school_event_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_term_planner_school_event_id_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_planner_week_planner_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_planner_week_planner_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_term_planner_week_planner_id_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "week_planner_lesson_plan_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week_planner_lesson_plan_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_week_planner_lesson_plan_id_week_planner_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "week_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "week_planner_school_event_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week_planner_school_event_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_week_planner_school_event_id_week_planner_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "week_planner",
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

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_school_event_id_TermPlannerId",
                table: "term_planner_school_event_id",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_week_planner_id_TermPlannerId",
                table: "term_planner_week_planner_id",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_lesson_plan_id_WeekPlannerId",
                table: "week_planner_lesson_plan_id",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_school_event_id_WeekPlannerId",
                table: "week_planner_school_event_id",
                column: "WeekPlannerId");

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
                name: "FK_Strand_year_level_YearLevel",
                table: "Strand",
                column: "YearLevel",
                principalTable: "year_level",
                principalColumn: "YearLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessment_summative_assessment_result_ResultId",
                table: "summative_assessment",
                column: "ResultId",
                principalTable: "summative_assessment_result",
                principalColumn: "SummativeAssessmentResultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TermPlan_term_plan_YearPlannerId",
                table: "TermPlan",
                column: "YearPlannerId",
                principalTable: "term_plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_year_level_Subjects_Subject",
                table: "year_level",
                column: "Subject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Strand_year_level_YearLevel",
                table: "Strand");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessment_summative_assessment_result_ResultId",
                table: "summative_assessment");

            migrationBuilder.DropForeignKey(
                name: "FK_TermPlan_term_plan_YearPlannerId",
                table: "TermPlan");

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
                name: "student_assessment_id");

            migrationBuilder.DropTable(
                name: "student_report_id");

            migrationBuilder.DropTable(
                name: "student_subject_id");

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

            migrationBuilder.DropTable(
                name: "term_planner_school_event_id");

            migrationBuilder.DropTable(
                name: "term_planner_week_planner_id");

            migrationBuilder.DropTable(
                name: "week_planner_lesson_plan_id");

            migrationBuilder.DropTable(
                name: "week_planner_school_event_id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_year_level",
                table: "year_level");

            migrationBuilder.DropPrimaryKey(
                name: "PK_week_planner",
                table: "week_planner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_term_planner",
                table: "term_planner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_term_plan",
                table: "term_plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment_result",
                table: "summative_assessment_result");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment",
                table: "summative_assessment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_report",
                table: "report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_content_descriptor",
                table: "content_descriptor");

            migrationBuilder.RenameTable(
                name: "year_level",
                newName: "YearLevel");

            migrationBuilder.RenameTable(
                name: "week_planner",
                newName: "WeekPlanners");

            migrationBuilder.RenameTable(
                name: "term_planner",
                newName: "TermPlanners");

            migrationBuilder.RenameTable(
                name: "term_plan",
                newName: "YearPlanners");

            migrationBuilder.RenameTable(
                name: "summative_assessment_result",
                newName: "SummativeAssessmentResult");

            migrationBuilder.RenameTable(
                name: "summative_assessment",
                newName: "SummativeAssessments");

            migrationBuilder.RenameTable(
                name: "report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "content_descriptor",
                newName: "ContentDescriptor");

            migrationBuilder.RenameIndex(
                name: "IX_year_level_Subject",
                table: "YearLevel",
                newName: "IX_YearLevel_Subject");

            migrationBuilder.RenameIndex(
                name: "IX_summative_assessment_ResultId",
                table: "SummativeAssessments",
                newName: "IX_SummativeAssessments_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptor_Substrand",
                table: "ContentDescriptor",
                newName: "IX_ContentDescriptor_Substrand");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptor_Strand",
                table: "ContentDescriptor",
                newName: "IX_ContentDescriptor_Strand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YearLevel",
                table: "YearLevel",
                column: "YearLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeekPlanners",
                table: "WeekPlanners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TermPlanners",
                table: "TermPlanners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YearPlanners",
                table: "YearPlanners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummativeAssessmentResult",
                table: "SummativeAssessmentResult",
                column: "SummativeAssessmentResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummativeAssessments",
                table: "SummativeAssessments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentDescriptor",
                table: "ContentDescriptor",
                column: "ContentDescriptorId");

            migrationBuilder.CreateTable(
                name: "LessonAssessmentId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAssessmentId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAssessmentId_LessonPlans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonResourceId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonResourceId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonResourceId_LessonPlans_LessonId",
                        column: x => x.LessonId,
                        principalTable: "LessonPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportComment",
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
                    table.PrimaryKey("PK_ReportComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportComment_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentAssessmentId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessmentId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssessmentId_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentReportId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReportId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentReportId_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentSubjectId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSubjectId_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherAssessmentId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherAssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAssessmentId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAssessmentId_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherReportId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherReportId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherReportId_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherResourceId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherResourceId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherResourceId_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherStudentId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherStudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudentId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherStudentId_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeacherSubjectId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjectId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjectId_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TermPlannerSchoolEventId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermPlannerSchoolEventId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermPlannerSchoolEventId_TermPlanners_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "TermPlanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TermPlannerWeekPlannerId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerI = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermPlannerWeekPlannerId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermPlannerWeekPlannerId_TermPlanners_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "TermPlanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeekPlannerLessonPlanId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlannerLessonPlanId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekPlannerLessonPlanId_WeekPlanners_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "WeekPlanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeekPlannerSchoolEventId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlannerSchoolEventId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekPlannerSchoolEventId_WeekPlanners_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "WeekPlanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAssessmentId_LessonId",
                table: "LessonAssessmentId",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonResourceId_LessonId",
                table: "LessonResourceId",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportComment_ReportId",
                table: "ReportComment",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessmentId_StudentId",
                table: "StudentAssessmentId",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentReportId_StudentId",
                table: "StudentReportId",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectId_StudentId",
                table: "StudentSubjectId",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAssessmentId_TeacherId",
                table: "TeacherAssessmentId",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherReportId_TeacherId",
                table: "TeacherReportId",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherResourceId_TeacherId",
                table: "TeacherResourceId",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudentId_TeacherId",
                table: "TeacherStudentId",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjectId_TeacherId",
                table: "TeacherSubjectId",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TermPlannerSchoolEventId_TermPlannerId",
                table: "TermPlannerSchoolEventId",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_TermPlannerWeekPlannerId_TermPlannerId",
                table: "TermPlannerWeekPlannerId",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlannerLessonPlanId_WeekPlannerId",
                table: "WeekPlannerLessonPlanId",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlannerSchoolEventId_WeekPlannerId",
                table: "WeekPlannerSchoolEventId",
                column: "WeekPlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentDescriptor_Strand_Strand",
                table: "ContentDescriptor",
                column: "Strand",
                principalTable: "Strand",
                principalColumn: "StrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentDescriptor_Substrand_Substrand",
                table: "ContentDescriptor",
                column: "Substrand",
                principalTable: "Substrand",
                principalColumn: "SubstrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Elaboration_ContentDescriptor_ContentDescriptor",
                table: "Elaboration",
                column: "ContentDescriptor",
                principalTable: "ContentDescriptor",
                principalColumn: "ContentDescriptorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Strand_YearLevel_YearLevel",
                table: "Strand",
                column: "YearLevel",
                principalTable: "YearLevel",
                principalColumn: "YearLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummativeAssessments_SummativeAssessmentResult_ResultId",
                table: "SummativeAssessments",
                column: "ResultId",
                principalTable: "SummativeAssessmentResult",
                principalColumn: "SummativeAssessmentResultId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TermPlan_YearPlanners_YearPlannerId",
                table: "TermPlan",
                column: "YearPlannerId",
                principalTable: "YearPlanners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearLevel_Subjects_Subject",
                table: "YearLevel",
                column: "Subject",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
