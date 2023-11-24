namespace TeachPlanner.Api.Contracts.Teachers.AccountSetup;

public record AccountSetupRequest(
    List<string> SubjectsTaught,
    List<string> YearLevelsTaught,
    DayPlanPatternDto DayPlanPattern);
