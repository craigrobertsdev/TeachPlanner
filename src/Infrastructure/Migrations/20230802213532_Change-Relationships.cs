using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "student_assessment_ids");

            migrationBuilder.DropTable(
                name: "student_report_ids");

            migrationBuilder.DropTable(
                name: "student_subject_ids");

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

            migrationBuilder.DropTable(
                name: "term_planner_school_event_id");

            migrationBuilder.DropTable(
                name: "term_planner_week_planner_id");

            migrationBuilder.DropTable(
                name: "TermPlan");

            migrationBuilder.DropTable(
                name: "week_planner_lesson_plan_id");

            migrationBuilder.DropTable(
                name: "week_planner_school_event_id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results");

            migrationBuilder.DropIndex(
                name: "IX_substrands_Strand",
                table: "substrands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_comment",
                table: "lesson_comment");

            migrationBuilder.DropIndex(
                name: "IX_content_descriptors_Strand",
                table: "content_descriptors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_term_plan",
                table: "term_plan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "ConductedDateTime",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "YearLevel",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "SummativeAssessmentResultId",
                table: "summative_assessment_results");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "summative_assessment_results");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "summative_assessment_results");

            migrationBuilder.DropColumn(
                name: "Strand",
                table: "substrands");

            migrationBuilder.DropColumn(
                name: "StrandId",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "ConductedDateTime",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "YearLevel",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "Strand",
                table: "content_descriptors");

            migrationBuilder.RenameTable(
                name: "term_plan",
                newName: "year_planner");

            migrationBuilder.RenameColumn(
                name: "YearLevelId",
                table: "year_levels",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "year_levels",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_year_levels_Subject",
                table: "year_levels",
                newName: "IX_year_levels_SubjectId");

            migrationBuilder.RenameColumn(
                name: "Grade_Id",
                table: "summative_assessment_results",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubstrandId",
                table: "substrands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "subjects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StrandId",
                table: "strands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "YearLevel",
                table: "strands",
                newName: "YearLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_strands_YearLevel",
                table: "strands",
                newName: "IX_strands_YearLevelId");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "report_comments",
                newName: "Guid");

            migrationBuilder.RenameColumn(
                name: "LessonCommentId",
                table: "lesson_comment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "lesson_comment",
                newName: "LessonPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_comment_LessonId",
                table: "lesson_comment",
                newName: "IX_lesson_comment_LessonPlanId");

            migrationBuilder.RenameColumn(
                name: "Elaboration",
                table: "elaborations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContentDescriptor",
                table: "elaborations",
                newName: "ContentDescriptorId");

            migrationBuilder.RenameIndex(
                name: "IX_elaborations_ContentDescriptor",
                table: "elaborations",
                newName: "IX_elaborations_ContentDescriptorId");

            migrationBuilder.RenameColumn(
                name: "ContentDescriptorId",
                table: "content_descriptors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Substrand",
                table: "content_descriptors",
                newName: "SubstrandId");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptors_Substrand",
                table: "content_descriptors",
                newName: "IX_content_descriptors_SubstrandId");

            migrationBuilder.AddColumn<Guid>(
                name: "TermPlannerId",
                table: "week_planner",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonPlanId",
                table: "summative_assessments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StrandId",
                table: "substrands",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "subjects",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Student",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "AssociatedStrands",
                table: "resources",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "WeekPlannerId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonPlanId",
                table: "formative_assessments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ContentDescriptorId1",
                table: "elaborations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StrandId",
                table: "content_descriptors",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SubstrandId1",
                table: "content_descriptors",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_comment",
                table: "lesson_comment",
                columns: new[] { "Id", "LessonPlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_year_planner",
                table: "year_planner",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConductedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonPlanResource",
                columns: table => new
                {
                    LessonPlan1Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanResource", x => new { x.LessonPlan1Id, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_LessonPlanResource_lesson_plans_LessonPlan1Id",
                        column: x => x.LessonPlan1Id,
                        principalTable: "lesson_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonPlanResource_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonPlanResource1",
                columns: table => new
                {
                    LessonPlan2Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Resource1Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanResource1", x => new { x.LessonPlan2Id, x.Resource1Id });
                    table.ForeignKey(
                        name: "FK_LessonPlanResource1_lesson_plans_LessonPlan2Id",
                        column: x => x.LessonPlan2Id,
                        principalTable: "lesson_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonPlanResource1_resources_Resource1Id",
                        column: x => x.Resource1Id,
                        principalTable: "resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "school_events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Location_StreetNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location_StreetName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location_Suburb = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullDay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EventStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_events", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.SubjectId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    YearPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Subjects = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_plans", x => new { x.Id, x.YearPlannerId });
                    table.ForeignKey(
                        name: "FK_term_plans_year_planner_YearPlannerId",
                        column: x => x.YearPlannerId,
                        principalTable: "year_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SchoolEventTermPlanner",
                columns: table => new
                {
                    SchoolEventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolEventTermPlanner", x => new { x.SchoolEventsId, x.TermPlannerId });
                    table.ForeignKey(
                        name: "FK_SchoolEventTermPlanner_school_events_SchoolEventsId",
                        column: x => x.SchoolEventsId,
                        principalTable: "school_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolEventTermPlanner_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SchoolEventWeekPlanner",
                columns: table => new
                {
                    SchoolEventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolEventWeekPlanner", x => new { x.SchoolEventsId, x.WeekPlannerId });
                    table.ForeignKey(
                        name: "FK_SchoolEventWeekPlanner_school_events_SchoolEventsId",
                        column: x => x.SchoolEventsId,
                        principalTable: "school_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolEventWeekPlanner_week_planner_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "week_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_TeacherId",
                table: "week_planner",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_TermPlannerId",
                table: "week_planner",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_summative_assessments_LessonPlanId",
                table: "summative_assessments",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_substrands_StrandId",
                table: "substrands",
                column: "StrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TeacherId",
                table: "Student",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_resources_SubjectId",
                table: "resources",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_StudentId",
                table: "reports",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_SubjectId",
                table: "reports",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_TeacherId",
                table: "reports",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_ResourceId",
                table: "lesson_plans",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_SubjectId",
                table: "lesson_plans",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_WeekPlannerId",
                table: "lesson_plans",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_formative_assessments_LessonPlanId",
                table: "formative_assessments",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_elaborations_ContentDescriptorId1",
                table: "elaborations",
                column: "ContentDescriptorId1");

            migrationBuilder.CreateIndex(
                name: "IX_content_descriptors_StrandId",
                table: "content_descriptors",
                column: "StrandId");

            migrationBuilder.CreateIndex(
                name: "IX_content_descriptors_SubstrandId1",
                table: "content_descriptors",
                column: "SubstrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_StudentId",
                table: "Assessment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_SubjectId",
                table: "Assessment",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_TeacherId",
                table: "Assessment",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanResource_ResourceId",
                table: "LessonPlanResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanResource1_Resource1Id",
                table: "LessonPlanResource1",
                column: "Resource1Id");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEventTermPlanner_TermPlannerId",
                table: "SchoolEventTermPlanner",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEventWeekPlanner_WeekPlannerId",
                table: "SchoolEventWeekPlanner",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_term_plans_YearPlannerId",
                table: "term_plans",
                column: "YearPlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptors_strands_StrandId",
                table: "content_descriptors",
                column: "StrandId",
                principalTable: "strands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptors_substrands_SubstrandId",
                table: "content_descriptors",
                column: "SubstrandId",
                principalTable: "substrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptors_substrands_SubstrandId1",
                table: "content_descriptors",
                column: "SubstrandId1",
                principalTable: "substrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptorId",
                table: "elaborations",
                column: "ContentDescriptorId",
                principalTable: "content_descriptors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptorId1",
                table: "elaborations",
                column: "ContentDescriptorId1",
                principalTable: "content_descriptors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_formative_assessments_Assessment_Id",
                table: "formative_assessments",
                column: "Id",
                principalTable: "Assessment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_formative_assessments_lesson_plans_LessonPlanId",
                table: "formative_assessments",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_comment_lesson_plans_LessonPlanId",
                table: "lesson_comment",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_resources_ResourceId",
                table: "lesson_plans",
                column: "ResourceId",
                principalTable: "resources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_subjects_SubjectId",
                table: "lesson_plans",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_week_planner_WeekPlannerId",
                table: "lesson_plans",
                column: "WeekPlannerId",
                principalTable: "week_planner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_Student_StudentId",
                table: "reports",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_subjects_SubjectId",
                table: "reports",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_teachers_TeacherId",
                table: "reports",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resources_subjects_SubjectId",
                table: "resources",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_strands_year_levels_YearLevelId",
                table: "strands",
                column: "YearLevelId",
                principalTable: "year_levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_teachers_TeacherId",
                table: "Student",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_substrands_strands_StrandId",
                table: "substrands",
                column: "StrandId",
                principalTable: "strands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessments_Assessment_Id",
                table: "summative_assessments",
                column: "Id",
                principalTable: "Assessment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessments_lesson_plans_LessonPlanId",
                table: "summative_assessments",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_summative_assessments_summative_assessment_results_ResultId",
                table: "summative_assessments",
                column: "ResultId",
                principalTable: "summative_assessment_results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_teachers_TeacherId",
                table: "week_planner",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_term_planner_TermPlannerId",
                table: "week_planner",
                column: "TermPlannerId",
                principalTable: "term_planner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_year_levels_subjects_SubjectId",
                table: "year_levels",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptors_strands_StrandId",
                table: "content_descriptors");

            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptors_substrands_SubstrandId",
                table: "content_descriptors");

            migrationBuilder.DropForeignKey(
                name: "FK_content_descriptors_substrands_SubstrandId1",
                table: "content_descriptors");

            migrationBuilder.DropForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptorId",
                table: "elaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_elaborations_content_descriptors_ContentDescriptorId1",
                table: "elaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_formative_assessments_Assessment_Id",
                table: "formative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_formative_assessments_lesson_plans_LessonPlanId",
                table: "formative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_comment_lesson_plans_LessonPlanId",
                table: "lesson_comment");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_resources_ResourceId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_subjects_SubjectId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_week_planner_WeekPlannerId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_Student_StudentId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_subjects_SubjectId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_teachers_TeacherId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_subjects_SubjectId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_strands_year_levels_YearLevelId",
                table: "strands");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_teachers_TeacherId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_substrands_strands_StrandId",
                table: "substrands");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessments_Assessment_Id",
                table: "summative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessments_lesson_plans_LessonPlanId",
                table: "summative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_summative_assessments_summative_assessment_results_ResultId",
                table: "summative_assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_teachers_TeacherId",
                table: "week_planner");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_term_planner_TermPlannerId",
                table: "week_planner");

            migrationBuilder.DropForeignKey(
                name: "FK_year_levels_subjects_SubjectId",
                table: "year_levels");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "LessonPlanResource");

            migrationBuilder.DropTable(
                name: "LessonPlanResource1");

            migrationBuilder.DropTable(
                name: "SchoolEventTermPlanner");

            migrationBuilder.DropTable(
                name: "SchoolEventWeekPlanner");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropTable(
                name: "term_plans");

            migrationBuilder.DropTable(
                name: "school_events");

            migrationBuilder.DropIndex(
                name: "IX_week_planner_TeacherId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_week_planner_TermPlannerId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_summative_assessments_LessonPlanId",
                table: "summative_assessments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results");

            migrationBuilder.DropIndex(
                name: "IX_substrands_StrandId",
                table: "substrands");

            migrationBuilder.DropIndex(
                name: "IX_Student_TeacherId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_resources_SubjectId",
                table: "resources");

            migrationBuilder.DropIndex(
                name: "IX_reports_StudentId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_reports_SubjectId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_reports_TeacherId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_ResourceId",
                table: "lesson_plans");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_SubjectId",
                table: "lesson_plans");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_WeekPlannerId",
                table: "lesson_plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_comment",
                table: "lesson_comment");

            migrationBuilder.DropIndex(
                name: "IX_formative_assessments_LessonPlanId",
                table: "formative_assessments");

            migrationBuilder.DropIndex(
                name: "IX_elaborations_ContentDescriptorId1",
                table: "elaborations");

            migrationBuilder.DropIndex(
                name: "IX_content_descriptors_StrandId",
                table: "content_descriptors");

            migrationBuilder.DropIndex(
                name: "IX_content_descriptors_SubstrandId1",
                table: "content_descriptors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_year_planner",
                table: "year_planner");

            migrationBuilder.DropColumn(
                name: "TermPlannerId",
                table: "week_planner");

            migrationBuilder.DropColumn(
                name: "LessonPlanId",
                table: "summative_assessments");

            migrationBuilder.DropColumn(
                name: "StrandId",
                table: "substrands");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "AssociatedStrands",
                table: "resources");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "WeekPlannerId",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "LessonPlanId",
                table: "formative_assessments");

            migrationBuilder.DropColumn(
                name: "ContentDescriptorId1",
                table: "elaborations");

            migrationBuilder.DropColumn(
                name: "StrandId",
                table: "content_descriptors");

            migrationBuilder.DropColumn(
                name: "SubstrandId1",
                table: "content_descriptors");

            migrationBuilder.RenameTable(
                name: "year_planner",
                newName: "term_plan");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "year_levels",
                newName: "YearLevelId");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "year_levels",
                newName: "Subject");

            migrationBuilder.RenameIndex(
                name: "IX_year_levels_SubjectId",
                table: "year_levels",
                newName: "IX_year_levels_Subject");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "summative_assessment_results",
                newName: "Grade_Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "substrands",
                newName: "SubstrandId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "subjects",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "strands",
                newName: "StrandId");

            migrationBuilder.RenameColumn(
                name: "YearLevelId",
                table: "strands",
                newName: "YearLevel");

            migrationBuilder.RenameIndex(
                name: "IX_strands_YearLevelId",
                table: "strands",
                newName: "IX_strands_YearLevel");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "report_comments",
                newName: "SubjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "lesson_comment",
                newName: "LessonCommentId");

            migrationBuilder.RenameColumn(
                name: "LessonPlanId",
                table: "lesson_comment",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_comment_LessonPlanId",
                table: "lesson_comment",
                newName: "IX_lesson_comment_LessonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "elaborations",
                newName: "Elaboration");

            migrationBuilder.RenameColumn(
                name: "ContentDescriptorId",
                table: "elaborations",
                newName: "ContentDescriptor");

            migrationBuilder.RenameIndex(
                name: "IX_elaborations_ContentDescriptorId",
                table: "elaborations",
                newName: "IX_elaborations_ContentDescriptor");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "content_descriptors",
                newName: "ContentDescriptorId");

            migrationBuilder.RenameColumn(
                name: "SubstrandId",
                table: "content_descriptors",
                newName: "Substrand");

            migrationBuilder.RenameIndex(
                name: "IX_content_descriptors_SubstrandId",
                table: "content_descriptors",
                newName: "IX_content_descriptors_Substrand");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "teachers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConductedDateTime",
                table: "summative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "summative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "summative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "summative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "summative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "summative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "YearLevel",
                table: "summative_assessments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "SummativeAssessmentResultId",
                table: "summative_assessment_results",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "summative_assessment_results",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "summative_assessment_results",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "Strand",
                table: "substrands",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "subjects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "StrandId",
                table: "resources",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConductedDateTime",
                table: "formative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "formative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "formative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "formative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "formative_assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "formative_assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "YearLevel",
                table: "formative_assessments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "Strand",
                table: "content_descriptors",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_summative_assessment_results",
                table: "summative_assessment_results",
                column: "SummativeAssessmentResultId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_comment",
                table: "lesson_comment",
                column: "LessonCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_term_plan",
                table: "term_plan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "lesson_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    LessonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                name: "student_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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

            migrationBuilder.CreateTable(
                name: "teacher_assessment_ids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherAssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherStudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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

            migrationBuilder.CreateTable(
                name: "term_planner_school_event_id",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolEventId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                name: "TermPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Subjects = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermPlan_term_plan_YearPlannerId",
                        column: x => x.YearPlannerId,
                        principalTable: "term_plan",
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
                name: "IX_substrands_Strand",
                table: "substrands",
                column: "Strand");

            migrationBuilder.CreateIndex(
                name: "IX_content_descriptors_Strand",
                table: "content_descriptors",
                column: "Strand");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_assessment_ids_LessonId",
                table: "lesson_assessment_ids",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_resource_ids_LessonId",
                table: "lesson_resource_ids",
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

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_school_event_id_TermPlannerId",
                table: "term_planner_school_event_id",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_week_planner_id_TermPlannerId",
                table: "term_planner_week_planner_id",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_TermPlan_YearPlannerId",
                table: "TermPlan",
                column: "YearPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_lesson_plan_id_WeekPlannerId",
                table: "week_planner_lesson_plan_id",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_school_event_id_WeekPlannerId",
                table: "week_planner_school_event_id",
                column: "WeekPlannerId");

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
    }
}
