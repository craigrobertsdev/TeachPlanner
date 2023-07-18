using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class SubstrandId : ValueObject
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

    public static SubstrandId Create(string strandId)
    {
        return new(Guid.Parse(strandId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
