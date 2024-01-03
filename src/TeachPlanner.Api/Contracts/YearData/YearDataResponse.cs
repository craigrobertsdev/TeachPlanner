using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Contracts.Subjects;
using TeachPlanner.Api.Contracts.TermPlanners;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Contracts.YearData;

public record YearDataResponse(
    Guid Id,
    int CalendarYear,
    List<Guid> StudentIds,
    List<SubjectResponse> Subjects,
    List<YearLevelValue> YearLevels,
    List<LessonPlanResponse> LessonPlans,
    List<WeekPlannerResponse> WeekPlanners,
    TermPlannerResponse TermPlanner);

public record SubjectReponse(string Name, List<YearDataContentDescription> ContentDescriptions);