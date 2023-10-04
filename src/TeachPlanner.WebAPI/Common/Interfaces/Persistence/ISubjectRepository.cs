using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ISubjectRepository
{
    Task<List<Subject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken);
    Task<List<Subject>> GetCurriculumSubjects(bool includeElaborations, CancellationToken cancellationToken);
    Task<List<Subject>> GetCurriculumSubjectNamesAndIds(CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsById(List<SubjectId> subjects, bool includeElaborations, CancellationToken cancellationToken);
}
