using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.YearDataRecord;

namespace TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
public record GetTeacherSettingsResult(
    List<Subject> CurriculumSubjects,
    YearData YearData);
