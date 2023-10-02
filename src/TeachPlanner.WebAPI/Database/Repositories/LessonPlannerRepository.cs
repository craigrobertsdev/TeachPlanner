using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Teachers;

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
