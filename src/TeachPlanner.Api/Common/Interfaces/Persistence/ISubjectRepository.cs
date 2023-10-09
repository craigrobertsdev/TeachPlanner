using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ISubjectRepository
{
    Task<List<CurriculumSubject>> GetCurriculumSubjects(bool includeElaborations, CancellationToken cancellationToken);
    Task<List<CurriculumSubject>> GetSubjectsById(List<CurriculumSubjectId> subjects, bool includeElaborations, CancellationToken cancellationToken);
}
