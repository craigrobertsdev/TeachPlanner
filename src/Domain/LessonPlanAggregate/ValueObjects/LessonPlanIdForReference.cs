using Domain.Common.Primatives;

namespace Domain.LessonPlanAggregate.ValueObjects;
public class LessonPlanIdForReference : ValueObject
{
    public Guid Value { get; private set; }

    private LessonPlanIdForReference(Guid value)
    {
        Value = value;
    }

    public static LessonPlanIdForReference Create()
    {
        return new(Guid.NewGuid());
    }

    public static LessonPlanIdForReference Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlanIdForReference() { }
}
