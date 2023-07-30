using Domain.Common.Primatives;

namespace Domain.YearPlannerAggregate.ValueObjects;

public class YearPlannerIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private YearPlannerIdForReference(Guid value)
    {
        Value = value;
    }

    public static YearPlannerIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static YearPlannerIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearPlannerIdForReference() { }
}
