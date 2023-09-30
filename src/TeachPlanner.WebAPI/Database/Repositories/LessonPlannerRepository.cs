using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Database.DbContexts;
using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Features.Common.Interfaces.Persistence;
using TeachPlanner.Infrastructure.Persistence.DbContexts;

namespace TeachPlanner.Api.Database.Repositories;

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

    public async Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(TeacherId teacherId, CancellationToken cancellationToken)
    {
        return await _context.LessonPlans.Where(lessonPlan => lessonPlan.TeacherId == teacherId).ToListAsync();
    }
}
