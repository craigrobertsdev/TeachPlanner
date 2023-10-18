namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerRequest(Guid TeacherId, int WeekNumber, int TermNumber, int Year);