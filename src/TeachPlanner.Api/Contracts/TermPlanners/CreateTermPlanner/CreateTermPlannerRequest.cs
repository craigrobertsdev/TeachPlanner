using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Contracts.TermPlanners.CreateTermPlanner;

public record CreateTermPlannerRequest(List<TermPlan> TermPlans, List<YearLevelValue> YearLevels);