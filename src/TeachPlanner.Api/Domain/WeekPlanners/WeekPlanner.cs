using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Planner;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.WeekPlanners;

public sealed class WeekPlanner : Entity<WeekPlannerId>, IAggregateRoot
{
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public YearDataId YearDataId { get; private set; }
    public DateTime WeekStart { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();
    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        WeekPlannerId id,
        YearDataId yearDataId,
        DateTime weekStart,
        List<LessonPlan> lessonPlans,
        List<SchoolEvent>? schoolEvents) : base(id)
    {
        YearDataId = yearDataId;
        WeekStart = weekStart;
        _lessonPlans = lessonPlans;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;

        if (schoolEvents is not null)
        {
            _schoolEvents = schoolEvents;
        }
    }

    public static WeekPlanner Create(
        YearDataId yearDataId,
        DateTime weekStart,
        List<LessonPlan> lessonPlans,
        List<SchoolEvent>? schoolEvents = null)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            yearDataId,
            weekStart,
            lessonPlans,
            schoolEvents);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
