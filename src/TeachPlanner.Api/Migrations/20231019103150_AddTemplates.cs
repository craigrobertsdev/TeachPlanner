using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_week_planner_WeekPlannerId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_calendar_CalendarId",
                table: "week_planner");

            migrationBuilder.DropTable(
                name: "SchoolEventWeekPlanner");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "assessments");

            migrationBuilder.RenameColumn(
                name: "CalendarId",
                table: "week_planner",
                newName: "WeekPlanPatternId");

            migrationBuilder.RenameIndex(
                name: "IX_week_planner_CalendarId",
                table: "week_planner",
                newName: "IX_week_planner_WeekPlanPatternId");

            migrationBuilder.AddColumn<int>(
                name: "TermNumber",
                table: "week_planner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "week_planner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekPlannerId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "day_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    WeekPlannerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_day_plans_week_planner_WeekPlannerId",
                        column: x => x.WeekPlannerId,
                        principalTable: "week_planner",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "week_planner_templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_week_planner_templates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DayPlanSchoolEvent",
                columns: table => new
                {
                    DayPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SchoolEventsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "day_plan_templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Periods = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeekPlannerTemplateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_day_plan_templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_day_plan_templates_week_planner_templates_WeekPlannerTemplat~",
                        column: x => x.WeekPlannerTemplateId,
                        principalTable: "week_planner_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_day_plan_templates_WeekPlannerTemplateId",
                table: "day_plan_templates",
                column: "WeekPlannerTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_day_plans_WeekPlannerId",
                table: "day_plans",
                column: "WeekPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_DayPlanSchoolEvent_SchoolEventsId",
                table: "DayPlanSchoolEvent",
                column: "SchoolEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_day_plans_WeekPlannerId",
                table: "lesson_plans",
                column: "WeekPlannerId",
                principalTable: "day_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlanPatternId",
                table: "week_planner",
                column: "WeekPlanPatternId",
                principalTable: "week_planner_templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_day_plans_WeekPlannerId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlanPatternId",
                table: "week_planner");

            migrationBuilder.DropTable(
                name: "day_plan_templates");

            migrationBuilder.DropTable(
                name: "DayPlanSchoolEvent");

            migrationBuilder.DropTable(
                name: "week_planner_templates");

            migrationBuilder.DropTable(
                name: "day_plans");

            migrationBuilder.DropColumn(
                name: "TermNumber",
                table: "week_planner");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "week_planner");

            migrationBuilder.RenameColumn(
                name: "WeekPlanPatternId",
                table: "week_planner",
                newName: "CalendarId");

            migrationBuilder.RenameIndex(
                name: "IX_week_planner_WeekPlanPatternId",
                table: "week_planner",
                newName: "IX_week_planner_CalendarId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WeekPlannerId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "assessments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "IX_SchoolEventWeekPlanner_WeekPlannerId",
                table: "SchoolEventWeekPlanner",
                column: "WeekPlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_week_planner_WeekPlannerId",
                table: "lesson_plans",
                column: "WeekPlannerId",
                principalTable: "week_planner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_calendar_CalendarId",
                table: "week_planner",
                column: "CalendarId",
                principalTable: "calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
