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
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
