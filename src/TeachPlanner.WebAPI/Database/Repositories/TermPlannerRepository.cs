using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Database.Repositories;
public class TermPlannerRepository : ITermPlannerRepository
{
    private ApplicationDbContext _context;

    public TermPlannerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TermPlanner?> GetById(TermPlannerId id, CancellationToken cancellationToken)
    {
        return await _context.TermPlanners.
            FirstOrDefaultAsync(tp => tp.Id == id, cancellationToken);
    }

    public void Add(TermPlanner termPlanner)
    {
        _context.TermPlanners.Add(termPlanner);
    }

    public async Task Delete(TermPlannerId id, CancellationToken cancellationToken)
    {
        var termPlanner = await GetById(id, cancellationToken);

        if (termPlanner == null)
        {
            return;
        }

        _context.TermPlanners.Remove(termPlanner);
    }
}
