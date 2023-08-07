using Domain.SubjectAggregates;

namespace Application.Common.Interfaces.Persistence;
public interface ICurriculumRepository
{
    Task SaveCurriculum(List<Subject> subjects);

    Task<List<Subject>> GetAllSubjects();
}
