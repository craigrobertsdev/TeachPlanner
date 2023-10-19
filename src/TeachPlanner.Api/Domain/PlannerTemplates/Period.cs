namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record Period(PeriodType PeriodType, DateTime StartTime, DateTime EndTime);