using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IPlannerTemplateRepository {
    Task<DayPlanTemplate?> GetByTeacherId(TeacherId teacherId, CancellationToken cancellationToken);
}
