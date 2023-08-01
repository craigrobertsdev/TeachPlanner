using Domain.Common.Planner;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate;
using Domain.TeacherAggregate;
using Domain.WeekPlannerAggregate;

namespace Domain.TimeTableAggregate;

public sealed class WeekPlanner : AggregateRoot<WeekPlannerId>
{
    private readonly List<LessonPlanId> _lessonPlanIds = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public DateTime WeekStart { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<LessonPlanId> LessonPlanIds => _lessonPlanIds.AsReadOnly();
    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        WeekPlannerId id,
        DateTime weekStart,
        TeacherId teacherId,
        List<LessonPlanId> lessonPlanIds,
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
        TeacherId teacherId,
        List<LessonPlanId> lessonPlanIds,
        List<SchoolEvent>? schoolEvents = null)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            weekStart,
            teacherId,
            lessonPlanIds,
            schoolEvents);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
