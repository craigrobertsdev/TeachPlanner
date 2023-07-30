using Domain.Common.Primatives;

namespace Domain.ResourceAggregate.ValueObjects;
public class ResourceIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private ResourceIdForReference(Guid value)
    {
        Value = value;
    }

    public static ResourceIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static ResourceIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ResourceIdForReference() { }
}
