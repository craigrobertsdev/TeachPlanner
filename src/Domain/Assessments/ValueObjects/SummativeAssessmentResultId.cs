using Domain.Common.Primatives;

namespace Domain.Assessments.ValueObjects;

public class SummativeAssessmentResultId : ValueObject
{
    public Guid Value { get; private set; }

    private SummativeAssessmentResultId(Guid value)
    {
        Value = value;
    }

    public static SummativeAssessmentResultId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
