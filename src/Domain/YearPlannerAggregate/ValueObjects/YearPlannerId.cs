using Domain.Common.Primatives;

namespace Domain.YearPlannerAggregate.ValueObjects;

public class YearPlannerId : ValueObject
{
    public Guid Value { get; private set; }

    private YearPlannerId(Guid value)
    {
        Value = value;
    }

    public static YearPlannerId Create()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
