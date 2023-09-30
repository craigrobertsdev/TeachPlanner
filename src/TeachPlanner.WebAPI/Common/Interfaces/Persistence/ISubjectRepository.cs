using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ISubjectRepository
{
    Task<List<Subject>> GetCurriculum(CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsById(List<SubjectId> subjectIds, CancellationToken cancellationToken);
}
