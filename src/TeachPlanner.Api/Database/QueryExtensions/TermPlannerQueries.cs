using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class TermPlannerQueries
{

    public async static Task<TermPlanner?> GetTermPlannerById(this ApplicationDbContext context,
        TermPlannerId termPlannerId, CancellationToken cancellationToken)
    {
        return await context.TermPlanners
            .AsNoTracking()
            .Where(tp => tp.Id == termPlannerId)
            .Include(tp => tp.TermPlans)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
