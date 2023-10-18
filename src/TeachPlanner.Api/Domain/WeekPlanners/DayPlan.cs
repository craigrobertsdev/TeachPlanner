using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.LessonPlans;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public class DayPlan : Entity<DayPlanId>
{
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<SchoolEvent> _schoolEvents = new();

    private DayPlan(DayPlanId id, DateOnly date, List<LessonPlan> lessonPlans, List<SchoolEvent>? schoolEvents) :
        base(id)
    {
        _lessonPlans = lessonPlans;
        Date = date;

        if (schoolEvents is not null) _schoolEvents = schoolEvents;
    }

    public DateOnly Date { get; private set; }
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();

    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();

    public static DayPlan Create(DateOnly date, List<LessonPlan> lessonPlans, List<SchoolEvent> schoolEvents)
    {
        return new DayPlan(new DayPlanId(Guid.NewGuid()), date, lessonPlans, schoolEvents);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DayPlan()
    {
    }
}