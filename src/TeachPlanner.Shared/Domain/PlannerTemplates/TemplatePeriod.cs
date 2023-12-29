namespace TeachPlanner.Shared.Domain.PlannerTemplates;

public record TemplatePeriod(PeriodType PeriodType, string Name, TimeOnly StartTime, TimeOnly EndTime);