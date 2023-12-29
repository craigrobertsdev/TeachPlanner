using TeachPlanner.Shared.Domain.Common.Planner;
using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.LessonPlans;

namespace TeachPlanner.Shared.Domain.WeekPlanners;

public class DayPlan : Entity<DayPlanId> {
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public WeekPlannerId WeekPlannerId { get; private set; }
    public DateOnly Date { get; private set; }
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();

    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();

    private DayPlan(DayPlanId id, WeekPlannerId weekPlannerId, DateOnly date, List<LessonPlan> lessonPlans, List<SchoolEvent>? schoolEvents) :
        base(id) {
        _lessonPlans = lessonPlans;
        WeekPlannerId = weekPlannerId;
        Date = date;

        if (schoolEvents is not null) _schoolEvents = schoolEvents;
    }

    public static DayPlan Create(DateOnly date, WeekPlannerId weekPlannerId, List<LessonPlan> lessonPlans, List<SchoolEvent> schoolEvents) {
        return new DayPlan(new DayPlanId(Guid.NewGuid()), weekPlannerId, date, lessonPlans, schoolEvents);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private DayPlan() {
    }
}