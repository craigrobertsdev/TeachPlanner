using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;
public class StrandIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private StrandIdForReference(Guid value)
    {
        Value = value;
    }

    public static StrandIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static StrandIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private StrandIdForReference() { }
}
