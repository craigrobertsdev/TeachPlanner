using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ILessonPlannerRepository
{
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByTeacherIdAsync(TeacherId teacherId, CancellationToken cancellationToken);
}
