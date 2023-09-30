using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface ICurriculumRepository
{
    Task SaveCurriculum(List<Subject> subjects, CancellationToken cancellationToken);

    Task<List<Subject>> GetSubjects(bool elaborations, CancellationToken cancellationToken);
    Task<List<Subject>> GetCurriculumSubjectNamesAndIds(CancellationToken cancellationToken);
}
