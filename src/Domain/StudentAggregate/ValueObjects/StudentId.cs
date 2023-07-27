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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
