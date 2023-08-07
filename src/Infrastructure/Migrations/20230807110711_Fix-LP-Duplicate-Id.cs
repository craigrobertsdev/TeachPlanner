using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixLPDuplicateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_resources_ResourceId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlanResource_lesson_plans_LessonPlan1Id",
                table: "LessonPlanResource");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlanResource_resources_ResourceId",
                table: "LessonPlanResource");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_ResourceId",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "lesson_plans");

            migrationBuilder.RenameColumn(
                name: "ResourceId",
                table: "LessonPlanResource",
                newName: "ResourcesId");

            migrationBuilder.RenameColumn(
                name: "LessonPlan1Id",
                table: "LessonPlanResource",
                newName: "LessonPlansId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonPlanResource_ResourceId",
                table: "LessonPlanResource",
                newName: "IX_LessonPlanResource_ResourcesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlanResource_lesson_plans_LessonPlansId",
                table: "LessonPlanResource",
                column: "LessonPlansId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlanResource_resources_ResourcesId",
                table: "LessonPlanResource",
                column: "ResourcesId",
                principalTable: "resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlanResource_lesson_plans_LessonPlansId",
                table: "LessonPlanResource");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonPlanResource_resources_ResourcesId",
                table: "LessonPlanResource");

            migrationBuilder.RenameColumn(
                name: "ResourcesId",
                table: "LessonPlanResource",
                newName: "ResourceId");

            migrationBuilder.RenameColumn(
                name: "LessonPlansId",
                table: "LessonPlanResource",
                newName: "LessonPlan1Id");

            migrationBuilder.RenameIndex(
                name: "IX_LessonPlanResource_ResourcesId",
                table: "LessonPlanResource",
                newName: "IX_LessonPlanResource_ResourceId");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_ResourceId",
                table: "lesson_plans",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_resources_ResourceId",
                table: "lesson_plans",
                column: "ResourceId",
                principalTable: "resources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlanResource_lesson_plans_LessonPlan1Id",
                table: "LessonPlanResource",
                column: "LessonPlan1Id",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonPlanResource_resources_ResourceId",
                table: "LessonPlanResource",
                column: "ResourceId",
                principalTable: "resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
