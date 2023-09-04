using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmailAsync(string email);
    Task<Teacher?> GetTeacherByIdAsync(Guid userId);
    Task<List<Subject>?> GetSubjectsTaughtByTeacher(Guid teacherId, bool? elaborations);
    Task<List<Subject>?> SetSubjectsTaughtByTeacher(Guid teacherId, List<string> subjectNames);
}
