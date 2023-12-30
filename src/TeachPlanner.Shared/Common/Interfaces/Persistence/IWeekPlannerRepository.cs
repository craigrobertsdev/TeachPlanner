using TeachPlanner.Shared.Domain.WeekPlanners;

namespace TeachPlanner.Shared.Common.Interfaces.Persistence;

public interface IWeekPlannerRepository {
    Task<WeekPlanner?> GetWeekPlanner(int weekNumber, int termNumber, int year,
        CancellationToken cancellationToken);

    void Add(WeekPlanner weekPlanner);
}