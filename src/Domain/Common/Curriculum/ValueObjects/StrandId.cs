using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class StrandId : ValueObject
{
    public Guid Value { get; private set; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private StrandId(Guid value)
    {
        Value = value;
    }

    public static StrandId Create()
    {
        return new(Guid.NewGuid());
    }

    public static StrandId Create(string strandId)
    {
        return new(Guid.Parse(strandId));
    }
}
