using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTemplateStructure2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlanPatternId",
                table: "week_planner");

            migrationBuilder.RenameColumn(
                name: "WeekPlanPatternId",
                table: "week_planner",
                newName: "WeekPlannerTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_week_planner_WeekPlanPatternId",
                table: "week_planner",
                newName: "IX_week_planner_WeekPlannerTemplateId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Period",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Period",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlannerTemplateId",
                table: "week_planner",
                column: "WeekPlannerTemplateId",
                principalTable: "week_planner_templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlannerTemplateId",
                table: "week_planner");

            migrationBuilder.RenameColumn(
                name: "WeekPlannerTemplateId",
                table: "week_planner",
                newName: "WeekPlanPatternId");

            migrationBuilder.RenameIndex(
                name: "IX_week_planner_WeekPlannerTemplateId",
                table: "week_planner",
                newName: "IX_week_planner_WeekPlanPatternId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "Period",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "Period",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_week_planner_templates_WeekPlanPatternId",
                table: "week_planner",
                column: "WeekPlanPatternId",
                principalTable: "week_planner_templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
