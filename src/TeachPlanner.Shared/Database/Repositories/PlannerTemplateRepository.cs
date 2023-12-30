using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Database.Repositories;

public class PlannerTemplateRepository : IPlannerTemplateRepository {
    private readonly ApplicationDbContext _context;

    public PlannerTemplateRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<DayPlanTemplate?> GetByTeacherId(TeacherId teacherId, CancellationToken cancellationToken) {
        return await _context.DayPlanTemplates
            .Where(dp => dp.TeacherId == teacherId)
            .Include(dp => dp.Periods)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
