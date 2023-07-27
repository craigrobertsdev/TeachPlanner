using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class StrandId : ValueObject
{
    public Guid Value { get; private set; }

    private StrandId(Guid value)
    {
        Value = value;
    }

    public static StrandId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
