using Microsoft.EntityFrameworkCore;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Shared.Domain.TermPlanners;

namespace TeachPlanner.Blazor.Database.QueryExtensions;

public static class TermPlannerQueries {
    public static async Task<TermPlanner?> GetTermPlannerById(this ApplicationDbContext context,
        TermPlannerId termPlannerId, CancellationToken cancellationToken) {
        return await context.TermPlanners
            .AsNoTracking()
            .Where(tp => tp.Id == termPlannerId)
            .Include(tp => tp.TermPlans)
            .FirstOrDefaultAsync(cancellationToken);
    }
}