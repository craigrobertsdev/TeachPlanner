using Domain.Common.Primatives;

namespace Domain.Assessments.ValueObjects;
public class AssessmentId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private AssessmentId(Guid value)
    {
        Value = value;
    }

    public static AssessmentId Create()
    {
        return new(Guid.NewGuid());
    }

    public static AssessmentId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AssessmentId() { }
}
