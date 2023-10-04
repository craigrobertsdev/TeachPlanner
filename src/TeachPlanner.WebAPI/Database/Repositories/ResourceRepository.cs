using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Resources;

namespace TeachPlanner.Api.Database.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Resource>> GetResourcesById(List<ResourceId> resourceIds, CancellationToken cancellationToken)
    {
        return await _context.Resources
            .Where(x => resourceIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}
