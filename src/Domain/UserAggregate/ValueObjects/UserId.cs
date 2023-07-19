using Domain.Common.Primatives;

namespace Domain.UserAggregate.ValueObjects;

public record UserId(Guid Value);

/*public class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create()
    {
        return new(Guid.NewGuid());
    }

    public static UserId Create(string userId)
    {
        return new(Guid.Parse(userId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
*/