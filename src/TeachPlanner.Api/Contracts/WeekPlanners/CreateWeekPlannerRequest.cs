namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record CreateWeekPlannerRequest(
    int WeekNumber,
    int TermNumber,
    int Year,
    WeekPlannerTemplateRequest WeekPlannerTemplate,
    DateOnly WeekStart);

public record WeekPlannerTemplateRequest(DayPlanTemplateRequest DayPlanTemplate);

public record DayPlanTemplateRequest(List<PeriodRequest> Periods);

public record PeriodRequest(string PeriodType, DateTime StartTime, DateTime EndTime);