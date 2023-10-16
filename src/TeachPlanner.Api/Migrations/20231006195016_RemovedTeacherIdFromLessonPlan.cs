using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTeacherIdFromLessonPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessments_lesson_plans_LessonPlanId",
                table: "assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans");

            migrationBuilder.DropIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "lesson_plans");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonPlanId",
                table: "assessments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_lesson_plans_LessonPlanId",
                table: "assessments",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessments_lesson_plans_LessonPlanId",
                table: "assessments");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "lesson_plans",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonPlanId",
                table: "assessments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_plans_TeacherId",
                table: "lesson_plans",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_lesson_plans_LessonPlanId",
                table: "assessments",
                column: "LessonPlanId",
                principalTable: "lesson_plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_teachers_TeacherId",
                table: "lesson_plans",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
