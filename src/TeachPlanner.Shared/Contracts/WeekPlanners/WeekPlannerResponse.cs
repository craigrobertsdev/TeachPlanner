using TeachPlanner.Shared.Domain.Common;
using TeachPlanner.Shared.Domain.Common.Planner;
using TeachPlanner.Shared.Domain.WeekPlanners;

namespace TeachPlanner.Shared.Contracts.WeekPlanners;

public record WeekPlannerResponse(
    List<DayPlan> DayPlans,
    DayPlanTemplateDto DayPlanPattern,
    DateOnly WeekStart,
    int WeekNumber);

public record SchoolEventResponse(
    Location Location,
    string Name,
    bool FullDay,
    DateTime EventStart,
    DateTime EventEnd) {
    public static List<SchoolEventResponse> CreateMany(IEnumerable<SchoolEvent> schoolEvents) {
        return schoolEvents.Select(se => new SchoolEventResponse(
            se.Location,
            se.Name,
            se.FullDay,
            se.EventStart,
            se.EventEnd)).ToList();
    }
}