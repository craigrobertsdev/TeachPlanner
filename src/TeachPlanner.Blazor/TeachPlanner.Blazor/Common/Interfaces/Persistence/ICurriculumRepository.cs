using TeachPlanner.Shared.Enums;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface ICurriculumRepository {
    Task AddCurriculum(List<CurriculumSubject> subjects, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetAllSubjects(CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsByName(List<string> subjectNames, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsByYearLevels(List<YearLevelValue> yearLevels, CancellationToken cancellationToken);
}