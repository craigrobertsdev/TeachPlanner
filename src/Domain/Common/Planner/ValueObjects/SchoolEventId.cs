using Domain.Common.Primatives;

namespace Domain.Common.Planner.ValueObjects;

public class SchoolEventId : ValueObject
{
    public Guid Value { get; private set; }

    private SchoolEventId(Guid value)
    {
        Value = value;
    }

    public static SchoolEventId Create()
    {
        return new(Guid.NewGuid());
    }

    public static SchoolEventId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SchoolEventId() { }
}
