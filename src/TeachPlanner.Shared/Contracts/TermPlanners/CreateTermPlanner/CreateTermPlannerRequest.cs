using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Domain.TermPlanners;

namespace TeachPlanner.Shared.Contracts.TermPlanners.CreateTermPlanner;

public record CreateTermPlannerRequest(List<TermPlan> TermPlans, List<YearLevelValue> YearLevels);