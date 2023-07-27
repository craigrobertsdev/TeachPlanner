using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class SubjectId : ValueObject
{
    public Guid Value { get; private set; }

    private SubjectId(Guid value)
    {
        Value = value;
    }

    public static SubjectId Create()
    {
        return new(Guid.NewGuid());
    }

    public static SubjectId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SubjectId() { }
}
