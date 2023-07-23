using Application.Common.Interfaces.Persistence;
using Domain.LessonPlanAggregate;

namespace Infrastructure.Persistence.Repositories;

public class LessonRepository : ILessonRepository
{
    public async Task Create(LessonPlan lesson)
    {
        await Task.CompletedTask;
    }
}
