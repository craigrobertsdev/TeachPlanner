using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class ContentDescriptor : ValueObject
{
    private readonly List<Elaboration> _elaborations = new();
    public string Description { get; private set; }
    public string CurriculumCode { get; private set; }
    public IReadOnlyList<Elaboration>? Elaborations => _elaborations.AsReadOnly();

    private ContentDescriptor(string description, string curriculumCode, List<Elaboration> elaborations)
    {
        Description = description;
        CurriculumCode = curriculumCode;
        _elaborations = elaborations;
    }

    public static ContentDescriptor Create(string description, string curriculumCode, List<Elaboration> elaborations)
    {
        return new(description, curriculumCode, elaborations);
    }

    public void AddElaboration(Elaboration elaboration)
    {
        _elaborations.Add(elaboration);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Description;
        yield return CurriculumCode;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContentDescriptor()
    {
    }
}
