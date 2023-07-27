using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class StrandId : ValueObject
{
    public Guid Value { get; private set; }

    private StrandId(Guid value)
    {
        Value = value;
    }

    public static StrandId Create()
    {
        return new(Guid.NewGuid());
    }

    public static StrandId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private StrandId() { }
}
