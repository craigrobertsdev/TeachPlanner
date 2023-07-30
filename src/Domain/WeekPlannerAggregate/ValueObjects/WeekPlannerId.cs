using Domain.Common.Primatives;

namespace Domain.TimeTableAggregate.ValueObjects;

public class WeekPlannerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private WeekPlannerId(Guid value)
    {
        Value = value;
    }

    public static WeekPlannerId Create()
    {
        return new(Guid.NewGuid());
    }

    public static WeekPlannerId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlannerId() { }
}
