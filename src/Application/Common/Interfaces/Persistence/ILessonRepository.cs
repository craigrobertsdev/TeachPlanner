using Domain.LessonAggregate;

namespace Application.Common.Interfaces.Persistence;

public interface ILessonRepository
{
    Task Create(Lesson lesson);
}
