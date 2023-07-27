using Domain.Common.Primatives;

namespace Domain.TimeTableAggregate.ValueObjects;

public class WeekPlannerId : ValueObject
{
    public Guid Value { get; private set; }

    private WeekPlannerId(Guid value)
    {
        Value = value;
    }

    public static WeekPlannerId Create()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
