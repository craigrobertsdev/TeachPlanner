using Domain.Common.Primatives;

namespace Domain.TermPlannerAggregate.ValueObjects;

public class TermPlannerId : ValueObject
{
    public Guid Value { get; private set; }

    private TermPlannerId(Guid value)
    {
        Value = value;
    }

    public static TermPlannerId Create()
    {
        return new(Guid.NewGuid());
    }

    public static TermPlannerId Create(Guid guid)
    {
        return new(guid);
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlannerId() { }
}
