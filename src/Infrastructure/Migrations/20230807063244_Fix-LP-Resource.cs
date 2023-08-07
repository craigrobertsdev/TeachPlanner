using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixLPResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonPlanResource1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanResource1_Resource1Id",
                table: "LessonPlanResource1",
                column: "Resource1Id");
        }
    }
}
