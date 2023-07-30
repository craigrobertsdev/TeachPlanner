
using Domain.Common.Primatives;

namespace Domain.LessonPlanAggregate.ValueObjects;

public class LessonPlanId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private LessonPlanId(Guid value)
    {
        Value = value;
    }

    public static LessonPlanId Create()
    {
        return new(Guid.NewGuid());
    }

    public static LessonPlanId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.    
    private LessonPlanId() { }
}
