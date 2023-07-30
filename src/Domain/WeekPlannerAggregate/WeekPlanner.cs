using Domain.Common.Planner.ValueObjects;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Domain.TermPlannerAggregate;
using Domain.TimeTableAggregate.ValueObjects;

namespace Domain.TimeTableAggregate;

public sealed class WeekPlanner : AggregateRoot<WeekPlannerId>
{
    private readonly List<LessonPlanIdForReference> _lessonPlanIds = new();
    private readonly List<SchoolEventIdForReference> _schoolEventIds = new();
    public DateTime WeekStart { get; private set; }
    public TeacherIdForReference TeacherId { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<LessonPlanIdForReference> LessonPlanIds => _lessonPlanIds.AsReadOnly();
    public IReadOnlyList<SchoolEventIdForReference> SchoolEventIds => _schoolEventIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        WeekPlannerId id,
        DateTime weekStart,
        TeacherIdForReference teacherId,
        List<LessonPlanIdForReference> lessonPlanIds,
        List<SchoolEventIdForReference>? schoolEventIds) : base(id)
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
        TeacherIdForReference teacherId,
        List<LessonPlanIdForReference> lessonPlanIds,
        List<SchoolEventIdForReference>? schoolEventIds = null)
    {
        return new WeekPlanner(
            WeekPlannerId.Create(),
            weekStart,
            teacherId,
            lessonPlanIds,
            schoolEventIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
