using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Database.Repositories;

public class WeekPlannerRepository : IWeekPlannerRepository
{
    private readonly ApplicationDbContext _context;
    public WeekPlannerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<WeekPlanner?> GetWeekPlanner(TeacherId teacherId, int weekNumber, int termNumber, int year, CancellationToken cancellationToken)
    {
        return await _context.WeekPlanners
            .Where(wp => wp.WeekNumber == weekNumber)
            .Where(wp => wp.TermNumber == termNumber)
            .Where(wp => wp.Year == year)
            .Include(wp => wp.DayPlans)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Add(WeekPlanner weekPlanner)
    {
        _context.WeekPlanners.Add(weekPlanner);
    }
}