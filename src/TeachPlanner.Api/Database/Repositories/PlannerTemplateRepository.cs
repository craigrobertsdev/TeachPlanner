using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Database.Repositories;

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
