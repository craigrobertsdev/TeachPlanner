using Domain.Common.Planner.Entities;
using Domain.Common.Primatives;
using Domain.TermPlannerAggregate.ValueObjects;
using Domain.TimeTableAggregate;

namespace Domain.TermPlannerAggregate;

public class TermPlanner : AggregateRoot<TermPlannerId>
{
    private readonly List<WeekPlanner> _weekPlanners = new();
    private readonly List<SchoolEvent> _schoolEvents = new();
    public int TermNumber { get; private set; }
    public DateTime TermStart { get; private set; }
    public DateTime TermEnd { get; private set; }
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners.AsReadOnly();
    public IReadOnlyList<SchoolEvent> SchoolEvents => _schoolEvents.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private TermPlanner(
        TermPlannerId id,
        List<WeekPlanner>? weekPlanners,
        List<SchoolEvent>? schoolEvents,
        int termNumber,
        DateTime termStart,
        DateTime termEnd,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        if (weekPlanners is not null)
        {
            _weekPlanners = weekPlanners;
        }

        if (schoolEvents is not null)
        {
            _schoolEvents = schoolEvents;
        }

        TermNumber = termNumber;
        TermStart = termStart;
        TermEnd = termEnd;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static TermPlanner Create(
        List<WeekPlanner> weekPlanners,
        List<SchoolEvent>? schoolEvents,
        int termNumber,
        DateTime termStart,
        DateTime termEnd,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new TermPlanner(
            new TermPlannerId(Guid.NewGuid()),
            weekPlanners,
            schoolEvents,
            termNumber,
            termStart,
            termEnd,
            createdDateTime,
            updatedDateTime);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}
