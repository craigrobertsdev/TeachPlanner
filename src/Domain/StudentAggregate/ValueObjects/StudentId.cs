using Domain.Common.Primatives;

namespace Domain.StudentAggregate.ValueObjects;
public class StudentId : ValueObject
{
    public Guid Value { get; private set; }

    private StudentId(Guid value)
    {
        Value = value;
    }

    public static StudentId Create()
    {
        return new(Guid.NewGuid());
    }

    public static StudentId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private StudentId() { }
}
