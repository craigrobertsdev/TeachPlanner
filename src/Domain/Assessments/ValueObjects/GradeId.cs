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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
