using Domain.Common.Primatives;

namespace Domain.Assessments.ValueObjects;

public class GradeId : ValueObject
{
    public Guid Value { get; private set; }

    private GradeId(Guid value)
    {
        Value = value;
    }

    public static GradeId Create()
    {
        return new(Guid.NewGuid());
    }

    public static GradeId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private GradeId() { }
}
