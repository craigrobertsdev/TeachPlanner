using Domain.Common.Primatives;

namespace Domain.Common.Planner.ValueObjects;

public class SchoolEventIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private SchoolEventIdForReference(Guid value)
    {
        Value = value;
    }

    public static SchoolEventIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static SchoolEventIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SchoolEventIdForReference() { }
}
