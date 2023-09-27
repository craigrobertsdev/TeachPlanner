using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Teacher?> GetById(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(Guid teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(Guid teacherId, CancellationToken cancellationToken);
    Task<YearData?> GetYearDataByTeacherIdAndYear(Guid teacherId, int calendarYear, CancellationToken cancellationToken);
}
