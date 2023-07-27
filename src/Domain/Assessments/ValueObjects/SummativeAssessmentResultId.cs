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

    public static SummativeAssessmentResultId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SummativeAssessmentResultId() { }
}
