using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetTeacherByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Teacher?> GetById(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(Guid teacherId, CancellationToken cancellationToken);
    void SetSubjectsTaughtByTeacher(Teacher teacher, List<Subject> subjects, int calendarYear);
}
