using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;

public class LessonPlanRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(LessonPlan lessonPlan, CancellationToken cancellationToken)
    {
        _context.Add(lessonPlan);
        await _context.SaveChangesAsync();
    }

    public async Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken)
    {
        return await _context.LessonPlans.Where(lessonPlan => lessonPlan.TeacherId == teacherId).ToListAsync();
    }
}
