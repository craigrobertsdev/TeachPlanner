using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDomainMapping3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_lesson_plans_SummativeAssessment_LessonPlanId",
                table: "Assessment");

            migrationBuilder.DropIndex(
                name: "IX_Assessment_SummativeAssessment_LessonPlanId",
                table: "Assessment");

            migrationBuilder.DropColumn(
                name: "SummativeAssessment_LessonPlanId",
                table: "Assessment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SummativeAssessment_LessonPlanId",
                table: "Assessment",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_SummativeAssessment_LessonPlanId",
                table: "Assessment",
                column: "SummativeAssessment_LessonPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_lesson_plans_SummativeAssessment_LessonPlanId",
                table: "Assessment",
                column: "SummativeAssessment_LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id");
        }
    }
}
