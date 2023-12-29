using Microsoft.EntityFrameworkCore;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Blazor.Database.Repositories;

public class LessonPlanRepository : ILessonPlanRepository {
    private readonly ApplicationDbContext _context;

    public LessonPlanRepository(ApplicationDbContext context) {
        _context = context;
    }

    public void Add(LessonPlan lessonPlan) {
        _context.Add(lessonPlan);
    }

    public async Task<List<LessonPlan>> GetByYearDataAndDate(YearDataId yearDataId, DateOnly date,
        CancellationToken cancellationToken) {
        return await _context.LessonPlans
            .Where(lp => lp.YearDataId == yearDataId)
            .Where(lp => lp.LessonDate == date)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<LessonPlan>?> GetLessonsByYearDataId(YearDataId yearDataId,
        CancellationToken cancellationToken) {
        return await _context.LessonPlans
            .Where(lessonPlan => lessonPlan.YearDataId == yearDataId)
            .ToListAsync(cancellationToken);
    }
}