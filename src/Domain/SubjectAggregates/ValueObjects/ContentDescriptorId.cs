using Domain.Common.Primatives;

namespace Domain.SubjectAggregates.ValueObjects;

public class ContentDescriptorId : ValueObject
{
    public Guid Value { get; private set; }

    private ContentDescriptorId(Guid value)
    {
        Value = value;
    }

    public static ContentDescriptorId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
