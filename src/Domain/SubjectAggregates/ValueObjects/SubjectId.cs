using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class SubjectId : ValueObject
{
    public Guid Value { get; private set; }

    private SubjectId(Guid value)
    {
        Value = value;
    }

    public static SubjectId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
