using Domain.Common.Primatives;

namespace Domain.TeacherAggregate.ValueObjects;

public class TeacherId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private TeacherId(Guid value)
    {
        Value = value;
    }

    public static TeacherId Create()
    {
        return new(Guid.NewGuid());
    }

    public static TeacherId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TeacherId() { }
}
