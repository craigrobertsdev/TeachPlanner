using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class ElaborationId : ValueObject
{
    public Guid Value { get; private set; }

    private ElaborationId(Guid value)
    {
        Value = value;
    }

    public static ElaborationId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
