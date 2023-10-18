using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerResponse(
    List<DayPlan> DayPlans,
    DayPlanTemplate DayPlanPattern,
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
