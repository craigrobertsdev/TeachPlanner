using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovedYearDataToAggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_term_planner_teachers_TeacherId",
                table: "term_planner");

            migrationBuilder.DropForeignKey(
                name: "FK_YearData_teachers_TeacherId",
                table: "YearData");

            migrationBuilder.DropIndex(
                name: "IX_term_planner_TeacherId",
                table: "term_planner");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "term_planner",
                newName: "YearDataId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "YearData",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "YearDataHistory",
                table: "teachers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_YearDataId",
                table: "term_planner",
                column: "YearDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_term_planner_YearData_YearDataId",
                table: "term_planner",
                column: "YearDataId",
                principalTable: "YearData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearData_teachers_TeacherId",
                table: "YearData",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_term_planner_YearData_YearDataId",
                table: "term_planner");

            migrationBuilder.DropForeignKey(
                name: "FK_YearData_teachers_TeacherId",
                table: "YearData");

            migrationBuilder.DropIndex(
                name: "IX_term_planner_YearDataId",
                table: "term_planner");

            migrationBuilder.DropColumn(
                name: "YearDataHistory",
                table: "teachers");

            migrationBuilder.RenameColumn(
                name: "YearDataId",
                table: "term_planner",
                newName: "TeacherId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "YearData",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_term_planner_TeacherId",
                table: "term_planner",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_term_planner_teachers_TeacherId",
                table: "term_planner",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_YearData_teachers_TeacherId",
                table: "YearData",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");
        }
    }
}
