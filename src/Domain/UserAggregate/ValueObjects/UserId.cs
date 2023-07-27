using Domain.Common.Primatives;

namespace Domain.UserAggregate.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; private set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
