using Domain.LessonPlanAggregate;

namespace Application.Common.Interfaces.Persistence;

public interface ILessonRepository
{
    Task Create(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId);
}
