using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IResourceRepository : IRepository<Resource>
{
    public Task<List<Resource>> GetResourcesById(
        List<ResourceId> resourceIds,
        CancellationToken cancellationToken
    );

    public Task<IEnumerable<Resource>> GetByTeacherAndSubject(
            TeacherId teacherId,
            SubjectId subjectId,
            CancellationToken cancellationToken
            );
}
