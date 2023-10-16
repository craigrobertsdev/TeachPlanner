using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Database.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _context;

    public ResourceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Resource>> GetResourcesById(
        List<ResourceId> resourceIds,
        CancellationToken cancellationToken
    )
    {
        return await _context.Resources
            .Where(x => resourceIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Resource>> GetByTeacherAndSubject(
        TeacherId teacherId,
        SubjectId subjectId,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .Where(t => t.Id == teacherId)
            .Include(t => t.Resources
                    .Where(r => r.SubjectId == subjectId))
            .FirstOrDefaultAsync(cancellationToken);

        return teacher != null ? teacher.Resources : new List<Resource>();
    }

    public void Add(Resource resource)
    {
        _context.Resources.Add(resource);
    }
}
