using Domain.Common.Primatives;

namespace Domain.TeacherAggregate.ValueObjects;
public class TeacherIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private TeacherIdForReference(Guid value)
    {
        Value = value;
    }

    public static TeacherIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static TeacherIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
