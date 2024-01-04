using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Shared.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "calendar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    TermStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "school_events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_StreetNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Location_StreetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location_Suburb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullDay = table.Column<bool>(type: "bit", nullable: false),
                    EventStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "term_dates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarSchoolEvent",
                columns: table => new
                {
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
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
                });

            migrationBuilder.CreateTable(
                name: "day_plan_templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_plan_templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_day_plan_templates_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "year_data_entries",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year_data_entries", x => new { x.TeacherId, x.Id });
                    table.ForeignKey(
                        name: "FK_year_data_entries_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DayPlanTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_template_periods_day_plan_templates_DayPlanTemplateId",
                        column: x => x.DayPlanTemplateId,
                        principalTable: "day_plan_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assessment_results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Grade_Grade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    DateMarked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_results", x => new { x.Id, x.AssessmentId });
                });

            migrationBuilder.CreateTable(
                name: "assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearLevel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AssessmentType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PlanningNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateConducted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assessments_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "content_descriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CurriculumCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Substrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_descriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "elaborations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContentDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "curriculum_subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TermPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculum_subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumSubjectTeacher",
                columns: table => new
                {
                    SubjectsTaughtId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsAssessment = table.Column<bool>(type: "bit", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociatedStrands = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
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
                });

            migrationBuilder.CreateTable(
                name: "year_levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearLevelValue = table.Column<int>(type: "int", nullable: true),
                    BandLevelValue = table.Column<int>(type: "int", nullable: true),
                    YearLevelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementStandard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurriculumSubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year_levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_year_levels_curriculum_subjects_CurriculumSubjectId",
                        column: x => x.CurriculumSubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "strands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "day_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeekPlannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayPlanSchoolEvent",
                columns: table => new
                {
                    DayPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
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
                });

            migrationBuilder.CreateTable(
                name: "lesson_comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    StruckOut = table.Column<bool>(type: "bit", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_comment", x => new { x.Id, x.LessonPlanId });
                });

            migrationBuilder.CreateTable(
                name: "lesson_plan_resources",
                columns: table => new
                {
                    LessonPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_plan_resources", x => new { x.LessonPlanId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_lesson_plan_resources_resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "lesson_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanningNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NumberOfPeriods = table.Column<int>(type: "int", nullable: false),
                    StartPeriod = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekPlannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lesson_plans_curriculum_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lesson_plans_day_plans_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "day_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CharacterLimit = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearLevel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reports_curriculum_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "curriculum_subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reports_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "year_data_content_descriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurriculumCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_year_data_content_descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_year_data_content_descriptions_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "term_planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearLevels = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_planner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "term_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermPlannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_term_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_term_plans_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "yeardata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayPlanTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermPlannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    YearLevels = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_yeardata_term_planner_TermPlannerId",
                        column: x => x.TermPlannerId,
                        principalTable: "term_planner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "week_planner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayPlanTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeekStart = table.Column<DateOnly>(type: "date", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                unique: true,
                filter: "[TermPlannerId] IS NOT NULL");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_students_StudentId",
                table: "assessments",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_yeardata_YearDataId",
                table: "lesson_plans",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id");

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
                principalColumn: "Id");

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
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teachers_AspNetUsers_UserId",
                table: "teachers");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "AspNetRoles");

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
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "yeardata");

            migrationBuilder.DropTable(
                name: "day_plan_templates");

            migrationBuilder.DropTable(
                name: "term_planner");
        }
    }
}
