using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ICurriculumRepository
{
    Task AddCurriculum(List<CurriculumSubject> subjects, CancellationToken cancellationToken);
}