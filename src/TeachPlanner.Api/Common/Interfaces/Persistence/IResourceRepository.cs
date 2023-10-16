using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IResourceRepository : IRepository<Resource>
{
    Task<List<Resource>> GetResourcesById(
        List<ResourceId> resourceIds,
        CancellationToken cancellationToken
    );

    Task<IEnumerable<Resource>> GetByTeacherAndSubject(
            TeacherId teacherId,
            SubjectId subjectId,
            CancellationToken cancellationToken
            );

    void Add(Resource resource);
}
