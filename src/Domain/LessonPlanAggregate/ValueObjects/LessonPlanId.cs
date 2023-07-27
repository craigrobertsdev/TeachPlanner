
using Domain.Common.Primatives;

namespace Domain.LessonPlanAggregate.ValueObjects;

public class LessonPlanId : ValueObject
{
    public Guid Value { get; private set; }

    private LessonPlanId(Guid value)
    {
        Value = value;
    }

    public static LessonPlanId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
