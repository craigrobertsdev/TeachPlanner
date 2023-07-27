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

    public static ReportId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ReportId() { }
}
