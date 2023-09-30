using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ILessonPlannerRepository
{
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(TeacherId teacherId, CancellationToken cancellationToken);
}
