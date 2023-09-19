using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;

public interface ISubjectRepository
{
    Task<List<Subject>> GetCurriculum(CancellationToken cancellationToken);
    Task<List<Subject>> GetSubjectsById(List<Guid> subjectIds, CancellationToken cancellationToken);
}
