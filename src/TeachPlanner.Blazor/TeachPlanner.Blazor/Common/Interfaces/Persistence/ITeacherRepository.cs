using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Users;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface ITeacherRepository {
    void Add(Teacher teacher);
    Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetByUserId(UserId userId, CancellationToken cancellationToken);
    Task<Teacher?> GetById(TeacherId teacherId, CancellationToken cancellationToken);
    Task<Teacher?> GetWithResources(TeacherId teacherId, CancellationToken cancellationToken);
    Task<IEnumerable<Resource>> GetResourcesBySubject(TeacherId teacherId, SubjectId subjectId,
        CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithoutElaborations(TeacherId teacherId,
        CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithElaborations(TeacherId teacherId,
        CancellationToken cancellationToken);
    Task<YearData?> GetYearDataByTeacherIdAndYear(TeacherId teacherId, int calendarYear,
        CancellationToken cancellationToken);
}