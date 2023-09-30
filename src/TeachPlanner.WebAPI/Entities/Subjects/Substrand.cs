using TeachPlanner.Api.Entities.Common.Primatives;

namespace TeachPlanner.Api.Entities.Subjects;

public sealed class Substrand : ValueObject
{
    private readonly List<ContentDescription> _contentDescriptions = new();
    public string Name { get; private set; }
    public Strand Strand { get; private set; }
    public IReadOnlyList<ContentDescription> ContentDescriptions => _contentDescriptions.AsReadOnly();

    private Substrand(string name, List<ContentDescription> contentDescriptions, Strand strand)
    {
        Name = name;
        _contentDescriptions = contentDescriptions;
        Strand = strand;
    }

    public static Substrand Create(string name, List<ContentDescription> contentDescriptions, Strand strand)
    {
        return new(name, contentDescriptions, strand);
    }

    public void AddContentDescription(ContentDescription contentDescription)
    {
        _contentDescriptions.Add(contentDescription);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return ContentDescriptions;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Substrand() { }
}
