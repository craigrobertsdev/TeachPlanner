using Domain.Common.Primatives;

namespace Domain.ResourceAggregate.ValueObjects;

public class ResourceId : ValueObject
{
    public Guid Value { get; private set; }

    private ResourceId(Guid value)
    {
        Value = value;
    }

    public static ResourceId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}