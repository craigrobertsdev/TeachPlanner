using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IWeekPlannerRepository
{
  Task<WeekPlanner> GetWeekPlanner(TeacherId teacherId, int weekNumber, int termNumber, int year, CancellationToken cancellationToken);
}