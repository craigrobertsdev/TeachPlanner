using Domain.Common.Primatives;

namespace Domain.YearPlannerAggregate.ValueObjects;

public class TermPlanId : ValueObject
{
    public Guid Value { get; private set; }

    private TermPlanId(Guid value)
    {
        Value = value;
    }

    public static TermPlanId Create()
    {
        return new(Guid.NewGuid());
    }

    public static TermPlanId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanId() { }
}
