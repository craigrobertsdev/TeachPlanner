using Domain.Common.Primatives;

namespace Domain.ReportAggregate.ValueObjects;

public class ReportCommentId : ValueObject
{
    public Guid Value { get; private set; }

    private ReportCommentId(Guid value)
    {
        Value = value;
    }

    public static ReportCommentId Create()
    {
        return new(Guid.NewGuid());
    }

    public static ReportCommentId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ReportCommentId() { }
}
