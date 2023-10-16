using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ILessonPlanRepository
{
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByYearDataId(YearDataId yearDataId, CancellationToken cancellationToken);
    Task<List<LessonPlan>> GetByYearDataAndDate(YearDataId yearDataId, DateOnly date, CancellationToken cancellationToken);
}
