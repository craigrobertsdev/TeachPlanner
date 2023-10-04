using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ILessonPlanRepository
{
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherId(TeacherId teacherId, CancellationToken cancellationToken);
}
