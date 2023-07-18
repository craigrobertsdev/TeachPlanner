using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class ContentDescriptorId : ValueObject
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

    public static ContentDescriptorId Create(string contentDescriptorId)
    {
        return new(Guid.Parse(contentDescriptorId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
