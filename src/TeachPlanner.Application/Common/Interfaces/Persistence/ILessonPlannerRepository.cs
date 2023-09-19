using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;

public interface ILessonPlannerRepository
{
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken);
}
