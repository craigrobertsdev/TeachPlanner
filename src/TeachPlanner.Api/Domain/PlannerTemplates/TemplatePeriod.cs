namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record TemplatePeriod(PeriodType PeriodType, string Name, TimeOnly StartTime, TimeOnly EndTime);