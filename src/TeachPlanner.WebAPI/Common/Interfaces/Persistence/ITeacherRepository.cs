using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface ITeacherRepository
{
    void Create(Teacher teacher);
    Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Teacher?> GetById(TeacherId teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithoutElaborations(TeacherId teacherId, CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsTaughtByTeacherWithElaborations(TeacherId teacherId, CancellationToken cancellationToken);
    Task<YearData?> GetYearDataByTeacherIdAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
}
