using Microsoft.EntityFrameworkCore;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;

public class LessonPlannerRepository : ILessonPlannerRepository
{
    private readonly ApplicationDbContext _context;

    public LessonPlannerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(LessonPlan lessonPlan)
    {
        _context.Add(lessonPlan);
    }

    public async Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken)
    {
        return await _context.LessonPlans.Where(lessonPlan => lessonPlan.TeacherId == teacherId).ToListAsync();
    }
}
