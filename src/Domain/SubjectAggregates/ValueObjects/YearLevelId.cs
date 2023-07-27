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

    public static YearLevelId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevelId() { }
}
