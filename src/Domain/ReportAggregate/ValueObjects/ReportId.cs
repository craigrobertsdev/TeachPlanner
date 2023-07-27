using Domain.Common.Primatives;

namespace Domain.ReportAggregate.ValueObjects;

public class ReportId : ValueObject
{
    public Guid Value { get; private set; }

    private ReportId(Guid value)
    {
        Value = value;
    }

    public static ReportId Create()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
