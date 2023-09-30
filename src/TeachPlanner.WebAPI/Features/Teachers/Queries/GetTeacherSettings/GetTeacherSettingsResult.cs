using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Features.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsResult(
    List<Subject> CurriculumSubjects,
    YearData YearData);
