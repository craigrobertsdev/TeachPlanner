using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;

public interface ILessonRepository
{
    Task Create(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId);
}
