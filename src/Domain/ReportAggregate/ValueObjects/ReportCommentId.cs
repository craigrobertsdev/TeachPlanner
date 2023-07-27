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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
