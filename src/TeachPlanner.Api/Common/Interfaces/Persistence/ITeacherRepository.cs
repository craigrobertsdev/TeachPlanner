using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ITeacherRepository
{
    void Add(Teacher teacher);
    Task<Teacher?> GetByEmail(string email, CancellationToken cancellationToken);
    Task<Teacher?> GetByUserId(UserId userId, CancellationToken cancellationToken);
    Task<Teacher?> GetById(TeacherId teacherId, CancellationToken cancellationToken);

    Task<IEnumerable<Resource>> GetResourcesBySubject(TeacherId teacherId, SubjectId subjectId,
        CancellationToken cancellationToken);

    Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithoutElaborations(TeacherId teacherId,
        CancellationToken cancellationToken);

    Task<List<CurriculumSubject>> GetSubjectsTaughtByTeacherWithElaborations(TeacherId teacherId,
        CancellationToken cancellationToken);

    Task<YearData?> GetYearDataByTeacherIdAndYear(TeacherId teacherId, int calendarYear,
        CancellationToken cancellationToken);

    void SetInitialAccountDetails(TeacherId id, List<string> subjectsTaught, DayPlanTemplate dayPlanTemplate,
               List<TermDate> termDates, CancellationToken cancellationToken);
}