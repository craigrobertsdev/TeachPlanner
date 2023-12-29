using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface ILessonPlanRepository {
    void Add(LessonPlan lesson);
    Task<List<LessonPlan>?> GetLessonsByYearDataId(YearDataId yearDataId, CancellationToken cancellationToken);

    Task<List<LessonPlan>> GetByYearDataAndDate(YearDataId yearDataId, DateOnly date,
        CancellationToken cancellationToken);
}