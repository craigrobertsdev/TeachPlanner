using Domain.Common.Planner.ValueObjects;
using Domain.Common.Primatives;
using Domain.TermPlannerAggregate.ValueObjects;
using Domain.TimeTableAggregate.ValueObjects;

namespace Domain.TermPlannerAggregate;

public sealed class TermPlanner : AggregateRoot<TermPlannerId>
{
    private readonly List<WeekPlannerId> _weekPlannerIds = new();
    private readonly List<SchoolEventId> _schoolEventIds = new();
    public int TermNumber { get; private set; }
    public DateTime TermStart { get; private set; }
    public DateTime TermEnd { get; private set; }
    public IReadOnlyList<WeekPlannerId> WeekPlannerIds => _weekPlannerIds.AsReadOnly();
    public IReadOnlyList<SchoolEventId> SchoolEventIds => _schoolEventIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private TermPlanner(
        TermPlannerId id,
        List<WeekPlannerId>? weekPlannerIds,
        List<SchoolEventId>? schoolEventIds,
        int termNumber,
        DateTime termStart,
        DateTime termEnd,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        if (weekPlannerIds is not null)
        {
            _weekPlannerIds = weekPlannerIds;
        }

        if (schoolEventIds is not null)
        {
            _schoolEventIds = schoolEventIds;
        }

        TermNumber = termNumber;
        TermStart = termStart;
        TermEnd = termEnd;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static TermPlanner Create(
        List<WeekPlannerId> weekPlannerIds,
        List<SchoolEventId>? schoolEventIds,
        int termNumber,
        DateTime termStart,
        DateTime termEnd,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new TermPlanner(
            TermPlannerId.Create(),
            weekPlannerIds,
            schoolEventIds,
            termNumber,
            termStart,
            termEnd,
            createdDateTime,
            updatedDateTime);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}
