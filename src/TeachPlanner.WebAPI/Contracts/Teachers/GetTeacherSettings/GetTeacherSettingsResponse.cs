using TeachPlanner.Api.Entities.Students;
using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Contracts.Teachers.GetTeacherSettings;
public record GetTeacherSettingsResponse(
    List<Subject> CurriculumSubjects,
    List<Subject> SubjectsTaught,
    List<Student> Students,
    int CalendarYear);
