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
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
