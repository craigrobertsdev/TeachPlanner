namespace TeachPlanner.Api.Contracts.Teachers.AccountSetup;

public record AccountSetupRequest(
    List<string> SubjectsTaught,
    DayPlanPatternDto DayPlanPattern,
    List<TermDateDto> TermDates,
    int? CalendarYear);
