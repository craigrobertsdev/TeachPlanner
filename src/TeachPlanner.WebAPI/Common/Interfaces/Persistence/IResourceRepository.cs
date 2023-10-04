using TeachPlanner.Api.Domain.Resources;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IResourceRepository : IRepository<Resource>
{
    public Task<List<Resource>> GetResourcesById(List<ResourceId> resourceIds, CancellationToken cancellationToken);
}
