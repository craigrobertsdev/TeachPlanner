using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.WeekPlanners;

namespace TeachPlanner.Shared.Database.Repositories;

public class WeekPlannerRepository : IWeekPlannerRepository {
    private readonly ApplicationDbContext _context;
    public WeekPlannerRepository(ApplicationDbContext context) {
        _context = context;
    }
    public async Task<WeekPlanner?> GetWeekPlanner(int weekNumber, int termNumber, int year, CancellationToken cancellationToken) {
        return await _context.WeekPlanners
            .Where(wp => wp.WeekNumber == weekNumber)
            .Where(wp => wp.TermNumber == termNumber)
            .Where(wp => wp.Year == year)
            .Include(wp => wp.DayPlans)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(WeekPlanner weekPlanner) {
        _context.WeekPlanners.Add(weekPlanner);
    }
}