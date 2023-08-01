using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class Elaboration : ValueObject
{
    public string Description { get; private set; }

    private Elaboration(string description)
    {
        Description = description;
    }

    public static Elaboration Create(string description)
    {
        return new(description);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Description;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Elaboration() { }
}
