using Domain.Common.Primatives;

namespace Domain.StudentAggregate.ValueObjects;
public class StudentIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private StudentIdForReference(Guid value)
    {
        Value = value;
    }

    public static StudentIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static StudentIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private StudentIdForReference() { }
}
