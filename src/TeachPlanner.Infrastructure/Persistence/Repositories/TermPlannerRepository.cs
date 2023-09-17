using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;
public class TermPlannerRepository : ITermPlannerRepository
{
    private ApplicationDbContext _context;

    public TermPlannerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TermPlanner?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TermPlanners.
            FirstOrDefaultAsync(tp => tp.Id == id, cancellationToken);
    }

    public void Add(TermPlanner termPlanner)
    {
        _context.TermPlanners.Add(termPlanner);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var termPlanner = await Get(id, cancellationToken);

        if (termPlanner == null)
        {
            return;
        }

        _context.TermPlanners.Remove(termPlanner);
    }
}
