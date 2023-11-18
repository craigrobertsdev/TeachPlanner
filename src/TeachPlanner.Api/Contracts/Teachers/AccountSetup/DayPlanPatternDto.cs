namespace TeachPlanner.Api.Contracts.Teachers.AccountSetup;

public record DayPlanPatternDto(
    List<LessonTemplateDto> LessonTemplates,
    List<BreakTemplateDto> BreakTemplates
    );

public record LessonTemplateDto(
    PeriodTimeDto StartTime,
    PeriodTimeDto EndTime);

public record BreakTemplateDto(
    string Name,
    PeriodTimeDto StartTime,
    PeriodTimeDto EndTime);

public record PeriodTimeDto(
    int Hours,
    int Minutes,
    string Period);

public record TermDateDto(
    string StartDate,
    string EndDate);
