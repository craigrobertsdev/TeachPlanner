using Domain.Common.Primatives;

namespace Domain.UserAggregate.ValueObjects;
public class UserIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private UserIdForReference(Guid value)
    {
        Value = value;
    }

    public static UserIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static UserIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserIdForReference() { }
}
