using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher, CancellationToken cancellationToken);
    Task<Teacher?> GetTeacherByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetTeacherById(Guid userId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> SetSubjectsTaughtByTeacher(Guid teacherId, List<string> subjectNames, CancellationToken cancellationToken);
}
