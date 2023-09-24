using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsResult(
    List<Subject> CurriculumSubjects,
    YearData YearData);
