using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IPlannerTemplateRepository {
    Task<DayPlanTemplate?> GetByTeacherId(TeacherId teacherId, CancellationToken cancellationToken);
}
