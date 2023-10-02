using TeachPlanner.Api.Contracts.LessonPlans;
using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Entities.WeekPlanners;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerResponse(
    List<LessonPlanResponse> LessonPlans,
    List<SchoolEventResponse> SchoolEvents,
    DateTime WeekStart,
    int WeekNumber);

public record SchoolEventResponse(
    Location Location,
    string Name,
    bool FullDay,
    DateTime EventStart,
    DateTime EventEnd)
{
    public static List<SchoolEventResponse> CreateMany(IEnumerable<SchoolEvent> schoolEvents)
    {
        return schoolEvents.Select(se => new SchoolEventResponse(
            se.Location,
            se.Name,
            se.FullDay,
            se.EventStart,
            se.EventEnd)).ToList();
    }
}
