using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Shared.Database.Migrations {
    /// <inheritdoc />
    public partial class UpdatePlannerTemplate : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "calendar",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    TermStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TermEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_calendar", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "school_events",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Location_StreetNumber = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location_StreetName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location_Suburb = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullDay = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EventStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_school_events", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_dates",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_term_dates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table => {
                    table.PrimaryKey("PK_users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CalendarSchoolEvent",
                columns: table => new {
                    CalendarId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolEventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_CalendarSchoolEvent", x => new { x.CalendarId, x.SchoolEventsId });
                    table.ForeignKey(
                        name: "FK_CalendarSchoolEvent_calendar_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "calendar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarSchoolEvent_school_events_SchoolEventsId",
                        column: x => x.SchoolEventsId,
                        principalTable: "school_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table => {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teachers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "day_plan_templates",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_day_plan_templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_day_plan_templates_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_data_entries",
                columns: table => new {
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_year_data_entries", x => new { x.TeacherId, x.Id });
                    table.ForeignKey(
                        name: "FK_year_data_entries_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "template_periods",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PeriodType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    DayPlanTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_template_periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_template_periods_day_plan_templates_DayPlanTemplateId",
                        column: x => x.DayPlanTemplateId,
                        principalTable: "day_plan_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "assessment_results",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssessmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comments = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade_Grade = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Percentage = table.Column<double>(type: "double", nullable: false),
                    DateMarked = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_assessment_results", x => new { x.Id, x.AssessmentId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "assessments",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssessmentType = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlanningNotes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateConducted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assessments_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "content_descriptions",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurriculumCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Substrand = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StrandId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_content_descriptions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "elaborations",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentDescriptionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_elaborations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_elaborations_content_descriptions_ContentDescriptionId",
                        column: x => x.ContentDescriptionId,
                        principalTable: "content_descriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "curriculum_subjects",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TermPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_curriculum_subjects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CurriculumSubjectTeacher",
                columns: table => new {
                    SubjectsTaughtId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
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

            migrationBuilder.CreateTable(
                name: "resources",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAssessment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssociatedStrands = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resources_curriculum_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_resources_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_levels",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevelValue = table.Column<int>(type: "int", nullable: true),
                    BandLevelValue = table.Column<int>(type: "int", nullable: true),
                    YearLevelDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AchievementStandard = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurriculumSubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_year_levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_year_levels_curriculum_subjects_CurriculumSubjectId",
                        column: x => x.CurriculumSubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "strands",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearLevelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_strands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_strands_year_levels_YearLevelId",
                        column: x => x.YearLevelId,
                        principalTable: "year_levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "day_plans",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_day_plans", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DayPlanSchoolEvent",
                columns: table => new {
                    DayPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolEventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_DayPlanSchoolEvent", x => new { x.DayPlanId, x.SchoolEventsId });
                    table.ForeignKey(
                        name: "FK_DayPlanSchoolEvent_day_plans_DayPlanId",
                        column: x => x.DayPlanId,
                        principalTable: "day_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayPlanSchoolEvent_school_events_SchoolEventsId",
                        column: x => x.SchoolEventsId,
                        principalTable: "school_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_comment",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StruckOut = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_lesson_comment", x => new { x.Id, x.LessonPlanId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_plan_resources",
                columns: table => new {
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_lesson_plan_resources", x => new { x.LessonPlanId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_lesson_plan_resources_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_plans",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlanningNotes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NumberOfPeriods = table.Column<int>(type: "int", nullable: false),
                    StartPeriod = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_lesson_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_plans_curriculum_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lesson_plans_day_plans_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "day_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report_comments",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Grade = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comments = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CharacterLimit = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_report_comments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevel = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reports_curriculum_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reports_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_subjects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_data_content_descriptions",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CurriculumCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table => {
                    table.PrimaryKey("PK_year_data_content_descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_year_data_content_descriptions_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_planner",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearLevels = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table => {
                    table.PrimaryKey("PK_term_planner", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_plans",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_term_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_term_plans_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "yeardata",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DayPlanTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearLevels = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table => {
                    table.PrimaryKey("PK_yeardata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_yeardata_day_plan_templates_DayPlanTemplateId",
                        column: x => x.DayPlanTemplateId,
                        principalTable: "day_plan_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_yeardata_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_yeardata_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "week_planner",
                columns: table => new {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DayPlanTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekStart = table.Column<DateOnly>(type: "date", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_week_planner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_week_planner_day_plan_templates_DayPlanTemplateId",
                        column: x => x.DayPlanTemplateId,
                        principalTable: "day_plan_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_week_planner_yeardata_YearDataId",
                        column: x => x.YearDataId,
                        principalTable: "yeardata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_results_AssessmentId",
                table: "assessment_results",
                column: "AssessmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assessments_LessonPlanId",
                table: "assessments",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_StudentId",
                table: "assessments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_SubjectId",
                table: "assessments",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_TeacherId",
                table: "assessments",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarSchoolEvent_SchoolEventsId",
                table: "CalendarSchoolEvent",
                column: "SchoolEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_content_descriptions_StrandId",
                table: "content_descriptions",
                column: "StrandId");

            migrationBuilder.CreateIndex(
                name: "IX_curriculum_subjects_TermPlanId",
                table: "curriculum_subjects",
                column: "TermPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubjectTeacher_TeacherId",
                table: "CurriculumSubjectTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_day_plan_templates_TeacherId",
                table: "day_plan_templates",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_day_plans_WeekPlannerId",
                table: "day_plans",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_DayPlanSchoolEvent_SchoolEventsId",
                table: "DayPlanSchoolEvent",
                column: "SchoolEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_elaborations_ContentDescriptionId",
                table: "elaborations",
                column: "ContentDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_comment_LessonPlanId",
                table: "lesson_comment",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plan_resources_ResourceId",
                table: "lesson_plan_resources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_SubjectId",
                table: "lesson_plans",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_WeekPlannerId",
                table: "lesson_plans",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_YearDataId",
                table: "lesson_plans",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_report_comments_ReportId",
                table: "report_comments",
                column: "ReportId");

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
                name: "IX_resources_SubjectId",
                table: "resources",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_resources_TeacherId",
                table: "resources",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_strands_YearLevelId",
                table: "strands",
                column: "YearLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_students_TeacherId",
                table: "students",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_students_YearDataId",
                table: "students",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_YearDataId",
                table: "subjects",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_UserId",
                table: "teachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_template_periods_DayPlanTemplateId",
                table: "template_periods",
                column: "DayPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_YearDataId",
                table: "term_planner",
                column: "YearDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_term_plans_TermPlannerId",
                table: "term_plans",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_DayPlanTemplateId",
                table: "week_planner",
                column: "DayPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_YearDataId",
                table: "week_planner",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_year_data_content_descriptions_SubjectId",
                table: "year_data_content_descriptions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_year_levels_CurriculumSubjectId",
                table: "year_levels",
                column: "CurriculumSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_yeardata_DayPlanTemplateId",
                table: "yeardata",
                column: "DayPlanTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_yeardata_TeacherId",
                table: "yeardata",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_yeardata_TermPlannerId",
                table: "yeardata",
                column: "TermPlannerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_assessment_results_assessments_AssessmentId",
                table: "assessment_results",
                column: "AssessmentId",
                principalTable: "assessments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_curriculum_subjects_SubjectId",
                table: "assessments",
                column: "SubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_lesson_plans_LessonPlanId",
                table: "assessments",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_students_StudentId",
                table: "assessments",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_content_descriptions_strands_StrandId",
                table: "content_descriptions",
                column: "StrandId",
                principalTable: "strands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_curriculum_subjects_term_plans_TermPlanId",
                table: "curriculum_subjects",
                column: "TermPlanId",
                principalTable: "term_plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_day_plans_week_planner_WeekPlannerId",
                table: "day_plans",
                column: "WeekPlannerId",
                principalTable: "week_planner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_comment_lesson_plans_LessonPlanId",
                table: "lesson_comment",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plan_resources_lesson_plans_LessonPlanId",
                table: "lesson_plan_resources",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_yeardata_YearDataId",
                table: "lesson_plans",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_report_comments_reports_ReportId",
                table: "report_comments",
                column: "ReportId",
                principalTable: "reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_students_StudentId",
                table: "reports",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_students_yeardata_YearDataId",
                table: "students",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_yeardata_YearDataId",
                table: "subjects",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_term_planner_yeardata_YearDataId",
                table: "term_planner",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_day_plan_templates_teachers_TeacherId",
                table: "day_plan_templates");

            migrationBuilder.DropForeignKey(
                name: "FK_yeardata_teachers_TeacherId",
                table: "yeardata");

            migrationBuilder.DropForeignKey(
                name: "FK_term_planner_yeardata_YearDataId",
                table: "term_planner");

            migrationBuilder.DropTable(
                name: "assessment_results");

            migrationBuilder.DropTable(
                name: "CalendarSchoolEvent");

            migrationBuilder.DropTable(
                name: "CurriculumSubjectTeacher");

            migrationBuilder.DropTable(
                name: "DayPlanSchoolEvent");

            migrationBuilder.DropTable(
                name: "elaborations");

            migrationBuilder.DropTable(
                name: "lesson_comment");

            migrationBuilder.DropTable(
                name: "lesson_plan_resources");

            migrationBuilder.DropTable(
                name: "report_comments");

            migrationBuilder.DropTable(
                name: "template_periods");

            migrationBuilder.DropTable(
                name: "term_dates");

            migrationBuilder.DropTable(
                name: "year_data_content_descriptions");

            migrationBuilder.DropTable(
                name: "year_data_entries");

            migrationBuilder.DropTable(
                name: "assessments");

            migrationBuilder.DropTable(
                name: "calendar");

            migrationBuilder.DropTable(
                name: "school_events");

            migrationBuilder.DropTable(
                name: "content_descriptions");

            migrationBuilder.DropTable(
                name: "resources");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "lesson_plans");

            migrationBuilder.DropTable(
                name: "strands");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "day_plans");

            migrationBuilder.DropTable(
                name: "year_levels");

            migrationBuilder.DropTable(
                name: "week_planner");

            migrationBuilder.DropTable(
                name: "curriculum_subjects");

            migrationBuilder.DropTable(
                name: "term_plans");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "yeardata");

            migrationBuilder.DropTable(
                name: "day_plan_templates");

            migrationBuilder.DropTable(
                name: "term_planner");
        }
    }
}
