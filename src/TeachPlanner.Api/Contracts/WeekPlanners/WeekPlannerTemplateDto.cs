using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerTemplateDto(List<DayPlanDto> DayPlans);

public record DayPlanDto(List<string> Periods);