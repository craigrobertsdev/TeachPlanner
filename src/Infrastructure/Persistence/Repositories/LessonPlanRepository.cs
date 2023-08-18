using Application.Common.Interfaces.Persistence;
using Domain.LessonPlanAggregate;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId)
    {
        return await _context.LessonPlans.Where(lessonPlan => lessonPlan.TeacherId == teacherId).ToListAsync();
    }
}
