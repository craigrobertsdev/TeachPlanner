using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ICurriculumRepository
{
    Task SaveCurriculum(List<Subject> subjects, CancellationToken cancellationToken);

    Task<List<Subject>> GetSubjects(bool elaborations, CancellationToken cancellationToken);
}
