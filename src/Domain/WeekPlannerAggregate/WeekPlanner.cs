using Domain.Common.Planner.Entities;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate;
using Domain.TeacherAggregate.ValueObjects;
using Domain.TimeTableAggregate.ValueObjects;

namespace Domain.TimeTableAggregate;

public sealed class WeekPlanner : AggregateRoot<WeekPlannerId>
{
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public DateTime WeekStart { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public int WeekNumber { get; private set; }
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();
    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private WeekPlanner(
        WeekPlannerId id,
        DateTime weekStart,
        TeacherId teacherId,
        List<LessonPlan> lessonPlans,
        List<SchoolEvent>? schoolEvents) : base(id)
    {
        WeekStart = weekStart;
        TeacherId = teacherId;
        _lessonPlans = lessonPlans;
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
        List<LessonPlan> lessonPlans,
        List<SchoolEvent>? schoolEvents = null)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            weekStart,
            teacherId,
            lessonPlans,
            schoolEvents);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner() { }
}
