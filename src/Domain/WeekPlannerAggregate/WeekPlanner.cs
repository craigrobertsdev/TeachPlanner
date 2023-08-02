using Domain.Common.Planner;
using Domain.Common.Primatives;

namespace Domain.WeekPlannerAggregate;

public sealed class WeekPlanner : AggregateRoot
{
    private readonly List<Guid> _lessonPlanIds = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public DateTime WeekStart { get; private set; }
    public Guid TeacherId { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<Guid> LessonPlanIds => _lessonPlanIds.AsReadOnly();
    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        Guid id,
        DateTime weekStart,
        Guid teacherId,
        List<Guid> lessonPlanIds,
        List<SchoolEvent>? schoolEvents) : base(id)
    {
        WeekStart = weekStart;
        TeacherId = teacherId;
        _lessonPlanIds = lessonPlanIds;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;

        if (schoolEvents is not null)
        {
            _schoolEvents = schoolEvents;
        }
    }

    public static WeekPlanner Create(
        DateTime weekStart,
        Guid teacherId,
        List<Guid> lessonPlanIds,
        List<SchoolEvent>? schoolEvents = null)
    {
        return new WeekPlanner(
            Guid.NewGuid(),
            weekStart,
            teacherId,
            lessonPlanIds,
            schoolEvents);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
