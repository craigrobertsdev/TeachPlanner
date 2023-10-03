using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Contracts.TermPlanners;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.Common.Enums;

namespace TeachPlanner.Api.Contracts.YearData;

public record YearDataResponse(
    Guid Id,
    int CalendarYear,
    List<Guid> StudentIds,
    List<YearLevelValue> YearLevels,
    List<LessonPlanResponse> LessonPlans,
    List<WeekPlannerResponse> WeekPlanners,
    TermPlannerResponse TermPlanner);
