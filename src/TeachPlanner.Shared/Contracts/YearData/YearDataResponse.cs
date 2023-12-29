using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Contracts.LessonPlans;
using TeachPlanner.Shared.Contracts.Subjects;
using TeachPlanner.Shared.Contracts.TermPlanners;
using TeachPlanner.Shared.Contracts.WeekPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Contracts.YearData;

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