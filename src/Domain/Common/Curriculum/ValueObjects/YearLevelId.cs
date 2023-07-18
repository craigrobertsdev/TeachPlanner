using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class YearLevelId : ValueObject
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

    public static YearLevelId Create(string yearLevelId)
    {
        return new(Guid.Parse(yearLevelId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
