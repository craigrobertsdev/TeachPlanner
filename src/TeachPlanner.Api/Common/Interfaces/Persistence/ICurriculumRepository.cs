using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ICurriculumRepository
{
    Task AddCurriculum(List<CurriculumSubject> subjects, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsByName(List<string> subjectNames, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken);
}