using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class ElaborationId : ValueObject
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

    public static ElaborationId Create(string elaborationId)
    {
        return new(Guid.Parse(elaborationId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
