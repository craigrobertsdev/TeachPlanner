using Domain.Common.Planner.ValueObjects;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Domain.TimeTableAggregate.ValueObjects;

namespace Domain.TimeTableAggregate;

public sealed class WeekPlanner : AggregateRoot<WeekPlannerId>
{
    private readonly List<LessonPlanId> _lessonPlanIds = new();
    private readonly List<SchoolEventId> _schoolEventIds = new();
    public DateTime WeekStart { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<LessonPlanId> LessonPlanIds => _lessonPlanIds.AsReadOnly();
    public IReadOnlyList<LessonPlanId> SchoolEventIds => _lessonPlanIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        WeekPlannerId id,
        DateTime weekStart,
        TeacherId teacherId,
        List<LessonPlanId> lessonPlanIds,
        List<SchoolEventId>? schoolEventIds) : base(id)
    {
        WeekStart = weekStart;
        TeacherId = teacherId;
        _lessonPlanIds = lessonPlanIds;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;

        if (schoolEventIds is not null)
        {
            _schoolEventIds = schoolEventIds;
        }
    }

    public static WeekPlanner Create(
        DateTime weekStart,
        TeacherId teacherId,
        List<LessonPlanId> lessonPlanIds,
        List<SchoolEventId>? schoolEventIds = null)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            weekStart,
            teacherId,
            lessonPlanIds,
            schoolEventIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
