using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Shared.Domain.WeekPlanners;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IWeekPlannerRepository {
    Task<WeekPlanner?> GetWeekPlanner(int weekNumber, int termNumber, int year,
        CancellationToken cancellationToken);

    void Add(WeekPlanner weekPlanner);
}