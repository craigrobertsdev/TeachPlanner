using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class YearLevelId : ValueObject
{
    public Guid Value { get; private set; }

    private YearLevelId(Guid value)
    {
        Value = value;
    }

    public static YearLevelId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
