using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class Substrand : ValueObject
{
    private readonly List<ContentDescription> _contentDescriptions = new();
    public string Name { get; private set; }
    public IReadOnlyList<ContentDescription> ContentDescriptions => _contentDescriptions.AsReadOnly();

    private Substrand(string name, List<ContentDescription> contentDescriptions)
    {
        Name = name;
        _contentDescriptions = contentDescriptions;
    }

    public static Substrand Create(string name, List<ContentDescription> contentDescriptions)
    {
        return new(name, contentDescriptions);
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
