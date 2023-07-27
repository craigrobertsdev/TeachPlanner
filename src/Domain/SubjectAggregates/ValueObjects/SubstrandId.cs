using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class SubstrandId : ValueObject
{
    public Guid Value { get; private set; }

    private SubstrandId(Guid value)
    {
        Value = value;
    }

    public static SubstrandId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
