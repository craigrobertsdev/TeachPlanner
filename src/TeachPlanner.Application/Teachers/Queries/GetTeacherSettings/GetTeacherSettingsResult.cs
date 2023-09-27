using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsResult(
    List<Subject> CurriculumSubjects,
    YearData YearData);
