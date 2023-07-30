using Domain.Common.Primatives;

namespace Domain.WeekPlannerAggregate.ValueObjects;

public class WeekPlannerIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private WeekPlannerIdForReference(Guid value)
    {
        Value = value;
    }

    public static WeekPlannerIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static WeekPlannerIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlannerIdForReference() { }
}
