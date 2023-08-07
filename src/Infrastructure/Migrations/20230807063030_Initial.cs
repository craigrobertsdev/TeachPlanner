using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "school_events",
                columns: table => new
                {
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
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_events", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "summative_assessment_results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade_Grade = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade_Percentage = table.Column<double>(type: "double", nullable: true),
                    DateMarked = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_summative_assessment_results", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "term_planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    TermStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TermEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_planner", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year_planner", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "resources",
                columns: table => new
                {
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
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_resources_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevelValue = table.Column<int>(type: "int", nullable: true),
                    BandLevelValue = table.Column<int>(type: "int", nullable: true),
                    YearLevelDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AchievementStandard = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year_levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_year_levels_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
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
                name: "teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teachers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                name: "strands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearLevelId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
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
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id");
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
                name: "week_planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TermPlannerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week_planner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_week_planner_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_week_planner_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "substrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StrandId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_substrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_substrands_strands_StrandId",
                        column: x => x.StrandId,
                        principalTable: "strands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearLevel = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reports_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reports_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
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
                name: "lesson_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TeacherId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlanningNotes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResourceId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_plans_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lesson_plans_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lesson_plans_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lesson_plans_week_planner_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "week_planner",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "content_descriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurriculumCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubstrandId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    StrandId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_content_descriptions_strands_StrandId",
                        column: x => x.StrandId,
                        principalTable: "strands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_content_descriptions_substrands_SubstrandId",
                        column: x => x.SubstrandId,
                        principalTable: "substrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "report_comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                name: "formative_assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formative_assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_formative_assessments_Assessment_Id",
                        column: x => x.Id,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_formative_assessments_lesson_plans_LessonPlanId",
                        column: x => x.LessonPlanId,
                        principalTable: "lesson_plans",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lesson_comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StruckThrough = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_comment", x => new { x.Id, x.LessonPlanId });
                    table.ForeignKey(
                        name: "FK_lesson_comment_lesson_plans_LessonPlanId",
                        column: x => x.LessonPlanId,
                        principalTable: "lesson_plans",
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
                name: "summative_assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PlanningNotes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateScheduled = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResultId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LessonPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_summative_assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_summative_assessments_Assessment_Id",
                        column: x => x.Id,
                        principalTable: "Assessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_summative_assessments_lesson_plans_LessonPlanId",
                        column: x => x.LessonPlanId,
                        principalTable: "lesson_plans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_summative_assessments_summative_assessment_results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "summative_assessment_results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "elaborations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentDescriptionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elaborations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_elaborations_content_descriptions_ContentDescriptionId",
                        column: x => x.ContentDescriptionId,
                        principalTable: "content_descriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_content_descriptions_StrandId",
                table: "content_descriptions",
                column: "StrandId");

            migrationBuilder.CreateIndex(
                name: "IX_content_descriptions_SubstrandId",
                table: "content_descriptions",
                column: "SubstrandId");

            migrationBuilder.CreateIndex(
                name: "IX_elaborations_ContentDescriptionId",
                table: "elaborations",
                column: "ContentDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_formative_assessments_LessonPlanId",
                table: "formative_assessments",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_comment_LessonPlanId",
                table: "lesson_comment",
                column: "LessonPlanId");

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
                name: "IX_LessonPlanResource_ResourceId",
                table: "LessonPlanResource",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanResource1_Resource1Id",
                table: "LessonPlanResource1",
                column: "Resource1Id");

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
                name: "IX_SchoolEventTermPlanner_TermPlannerId",
                table: "SchoolEventTermPlanner",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolEventWeekPlanner_WeekPlannerId",
                table: "SchoolEventWeekPlanner",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_strands_YearLevelId",
                table: "strands",
                column: "YearLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TeacherId",
                table: "Student",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_substrands_StrandId",
                table: "substrands",
                column: "StrandId");

            migrationBuilder.CreateIndex(
                name: "IX_summative_assessments_LessonPlanId",
                table: "summative_assessments",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_summative_assessments_ResultId",
                table: "summative_assessments",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_teachers_UserId",
                table: "teachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_term_plans_YearPlannerId",
                table: "term_plans",
                column: "YearPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_TeacherId",
                table: "week_planner",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_TermPlannerId",
                table: "week_planner",
                column: "TermPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_year_levels_SubjectId",
                table: "year_levels",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "elaborations");

            migrationBuilder.DropTable(
                name: "formative_assessments");

            migrationBuilder.DropTable(
                name: "lesson_comment");

            migrationBuilder.DropTable(
                name: "LessonPlanResource");

            migrationBuilder.DropTable(
                name: "LessonPlanResource1");

            migrationBuilder.DropTable(
                name: "report_comments");

            migrationBuilder.DropTable(
                name: "SchoolEventTermPlanner");

            migrationBuilder.DropTable(
                name: "SchoolEventWeekPlanner");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropTable(
                name: "summative_assessments");

            migrationBuilder.DropTable(
                name: "term_plans");

            migrationBuilder.DropTable(
                name: "content_descriptions");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "school_events");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "lesson_plans");

            migrationBuilder.DropTable(
                name: "summative_assessment_results");

            migrationBuilder.DropTable(
                name: "year_planner");

            migrationBuilder.DropTable(
                name: "substrands");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "resources");

            migrationBuilder.DropTable(
                name: "week_planner");

            migrationBuilder.DropTable(
                name: "strands");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "term_planner");

            migrationBuilder.DropTable(
                name: "year_levels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "subjects");
        }
    }
}
