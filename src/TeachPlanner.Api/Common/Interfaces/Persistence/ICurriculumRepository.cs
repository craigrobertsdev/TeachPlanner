using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface ICurriculumRepository
{
    Task AddCurriculum(List<Subject> subjects, CancellationToken cancellationToken);
}
