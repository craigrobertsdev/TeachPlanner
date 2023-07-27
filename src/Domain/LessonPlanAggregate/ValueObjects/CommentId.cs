using Domain.Common.Primatives;

namespace Domain.LessonPlanAggregate.ValueObjects;

public class CommentId : ValueObject
{
    public Guid Value { get; private set; }

    private CommentId(Guid value)
    {
        Value = value;
    }

    public static CommentId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
