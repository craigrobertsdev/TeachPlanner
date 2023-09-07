using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ICurriculumRepository
{
    Task SaveCurriculum(List<Subject> subjects);

    Task<List<Subject>> GetSubjects(bool elaborations, List<Guid>? subjectsTaught = null);
}
