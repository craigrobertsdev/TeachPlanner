using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmailAsync(string email);
    Task<Teacher?> GetTeacherById(Guid userId);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(Guid teacherId);
    Task<List<Subject>> SetSubjectsTaughtByTeacher(Guid teacherId, List<string> subjectNames);
}
