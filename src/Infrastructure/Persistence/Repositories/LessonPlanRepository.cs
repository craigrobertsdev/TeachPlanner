using Application.Common.Interfaces.Persistence;
using Domain.LessonPlanAggregate;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;

public class LessonPlanRepository : ILessonRepository
{
    private readonly ApplicationDbContext _context;

    public LessonPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(LessonPlan lessonPlan)
    {
        _context.Add(lessonPlan);
        await _context.SaveChangesAsync();
    }
}
