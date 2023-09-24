using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Contracts.Teacher.GetTeacherSettings;
public record GetTeacherSettingsResponse(
    List<Subject> CurriculumSubjects,
    List<Subject> SubjectsTaught,
    List<Student> Students,
    int CalendarYear);
