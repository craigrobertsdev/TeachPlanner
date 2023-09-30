using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovedRelationshipsFromTeacherToYearData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_teachers_TeacherId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_week_planner_TeacherId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "week_planner",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "YearDataId",
                table: "week_planner",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "YearDataId",
                table: "reports",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "YearDataId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_YearDataId",
                table: "week_planner",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_YearDataId",
                table: "reports",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_YearDataId",
                table: "lesson_plans",
                column: "YearDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_YearData_YearDataId",
                table: "lesson_plans",
                column: "YearDataId",
                principalTable: "YearData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_YearData_YearDataId",
                table: "reports",
                column: "YearDataId",
                principalTable: "YearData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_YearData_YearDataId",
                table: "week_planner",
                column: "YearDataId",
                principalTable: "YearData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_YearData_YearDataId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_YearData_YearDataId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_week_planner_YearData_YearDataId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_week_planner_YearDataId",
                table: "week_planner");

            migrationBuilder.DropIndex(
                name: "IX_reports_YearDataId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_YearDataId",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "YearDataId",
                table: "week_planner");

            migrationBuilder.DropColumn(
                name: "YearDataId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "YearDataId",
                table: "lesson_plans");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "week_planner",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_week_planner_TeacherId",
                table: "week_planner",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_week_planner_teachers_TeacherId",
                table: "week_planner",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");
        }
    }
}
