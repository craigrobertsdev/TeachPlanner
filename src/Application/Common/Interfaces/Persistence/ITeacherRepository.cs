using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teacher;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmailAsync(string email);
    Task<Teacher?> GetTeacherByIdAsync(Guid userId);
    Task<List<Guid>?> GetSubjectsTaughtByTeacher(Guid teacherId);
}
