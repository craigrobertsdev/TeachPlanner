using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.ValueObjects;

public sealed class SubjectId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SubjectId(Guid value)
    {
        Value = value;
    }

    public static SubjectId Create()
    {
        return new(Guid.NewGuid());
    }

    public static SubjectId Create(string subjectId)
    {
        return new(Guid.Parse(subjectId));
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
