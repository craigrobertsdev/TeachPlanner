using Domain.Common.Primatives;

namespace Domain.ResourceAggregate.ValueObjects;

public class ResourceId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ResourceId(Guid value)
    {
        Value = value;
    }

    public static ResourceId Create()
    {
        return new(Guid.NewGuid());
    }

    public static ResourceId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ResourceId() { }
}