using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class MovedToCurriculumSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessments_subjects_SubjectId",
                table: "assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_subjects_SubjectId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_subjects_SubjectId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_yeardata_YearDataId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_subjects_SubjectId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_subjects_term_plans_TermPlanId",
                table: "subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_year_levels_subjects_SubjectId",
                table: "year_levels");

            migrationBuilder.DropTable(
                name: "SubjectYearData");

            migrationBuilder.DropIndex(
                name: "IX_subjects_TermPlanId",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_reports_YearDataId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "IsCurriculumSubject",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "TermPlanId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTime",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "YearDataId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "lesson_plans");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "year_levels",
                newName: "CurriculumSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_year_levels_SubjectId",
                table: "year_levels",
                newName: "IX_year_levels_CurriculumSubjectId");

            migrationBuilder.RenameColumn(
                name: "ConductedDateTime",
                table: "assessments",
                newName: "DateConducted");

            migrationBuilder.AddColumn<Guid>(
                name: "YearDataId",
                table: "subjects",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateOnly>(
                name: "LessonDate",
                table: "lesson_plans",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "StartPeriod",
                table: "lesson_plans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "curriculum_subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TermPlanId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_curriculum_subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_curriculum_subjects_term_plans_TermPlanId",
                        column: x => x.TermPlanId,
                        principalTable: "term_plans",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "year_data_content_descriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CurriculumCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_YearDataId",
                table: "subjects",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_curriculum_subjects_TermPlanId",
                table: "curriculum_subjects",
                column: "TermPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_year_data_content_descriptions_SubjectId",
                table: "year_data_content_descriptions",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_curriculum_subjects_SubjectId",
                table: "assessments",
                column: "SubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_curriculum_subjects_SubjectId",
                table: "lesson_plans",
                column: "SubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_curriculum_subjects_SubjectId",
                table: "reports",
                column: "SubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resources_curriculum_subjects_SubjectId",
                table: "resources",
                column: "SubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_yeardata_YearDataId",
                table: "subjects",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_year_levels_curriculum_subjects_CurriculumSubjectId",
                table: "year_levels",
                column: "CurriculumSubjectId",
                principalTable: "curriculum_subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessments_curriculum_subjects_SubjectId",
                table: "assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_plans_curriculum_subjects_SubjectId",
                table: "lesson_plans");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_curriculum_subjects_SubjectId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_resources_curriculum_subjects_SubjectId",
                table: "resources");

            migrationBuilder.DropForeignKey(
                name: "FK_subjects_yeardata_YearDataId",
                table: "subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_year_levels_curriculum_subjects_CurriculumSubjectId",
                table: "year_levels");

            migrationBuilder.DropTable(
                name: "curriculum_subjects");

            migrationBuilder.DropTable(
                name: "year_data_content_descriptions");

            migrationBuilder.DropIndex(
                name: "IX_subjects_YearDataId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "YearDataId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "LessonDate",
                table: "lesson_plans");

            migrationBuilder.DropColumn(
                name: "StartPeriod",
                table: "lesson_plans");

            migrationBuilder.RenameColumn(
                name: "CurriculumSubjectId",
                table: "year_levels",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_year_levels_CurriculumSubjectId",
                table: "year_levels",
                newName: "IX_year_levels_SubjectId");

            migrationBuilder.RenameColumn(
                name: "DateConducted",
                table: "assessments",
                newName: "ConductedDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "subjects",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCurriculumSubject",
                table: "subjects",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "TermPlanId",
                table: "subjects",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTime",
                table: "subjects",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "YearDataId",
                table: "reports",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "lesson_plans",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "lesson_plans",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SubjectYearData",
                columns: table => new
                {
                    SubjectsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    YearDataId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectYearData", x => new { x.SubjectsId, x.YearDataId });
                    table.ForeignKey(
                        name: "FK_SubjectYearData_subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectYearData_yeardata_YearDataId",
                        column: x => x.YearDataId,
                        principalTable: "yeardata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_TermPlanId",
                table: "subjects",
                column: "TermPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_YearDataId",
                table: "reports",
                column: "YearDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectYearData_YearDataId",
                table: "SubjectYearData",
                column: "YearDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_subjects_SubjectId",
                table: "assessments",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_plans_subjects_SubjectId",
                table: "lesson_plans",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_subjects_SubjectId",
                table: "reports",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_yeardata_YearDataId",
                table: "reports",
                column: "YearDataId",
                principalTable: "yeardata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_resources_subjects_SubjectId",
                table: "resources",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_term_plans_TermPlanId",
                table: "subjects",
                column: "TermPlanId",
                principalTable: "term_plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_year_levels_subjects_SubjectId",
                table: "year_levels",
                column: "SubjectId",
                principalTable: "subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
