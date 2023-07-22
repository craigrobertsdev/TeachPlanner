using Application.Common.Interfaces.Persistence;
using Domain.LessonAggregate;

namespace Infrastructure.Persistence.Repositories;

public class LessonRepository : ILessonRepository
{
    public async Task Create(Lesson lesson)
    {
        await Task.CompletedTask;
    }
}
