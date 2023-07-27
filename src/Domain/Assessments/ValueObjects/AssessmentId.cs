using Domain.Common.Primatives;

namespace Domain.Assessments.ValueObjects;
public class AssessmentId : ValueObject
{
    public Guid Value { get; private set; }

    private AssessmentId(Guid value)
    {
        Value = value;
    }

    public static AssessmentId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
