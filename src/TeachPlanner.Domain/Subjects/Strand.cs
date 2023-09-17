using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Subjects;

public sealed class Strand : ValueObject
{
    public string Name { get; private set; }
    public YearLevel YearLevel { get; private set; }
    // one of these properties will be null depending on the subject
    private readonly List<Substrand>? _substrands = new();
    private readonly List<ContentDescription>? _contentDescriptions = new();
    public IReadOnlyList<Substrand>? Substrands => _substrands?.AsReadOnly();
    public IReadOnlyList<ContentDescription>? ContentDescriptions => _contentDescriptions?.AsReadOnly();
    public bool HasSubstrands => _substrands is not null;

    private Strand(
        string name,
        YearLevel yearLevel,
        List<Substrand>? substrands = null,
        List<ContentDescription>? contentDescriptions = null
    )
    {
        Name = name;
        YearLevel = yearLevel;
        _substrands = substrands;
        _contentDescriptions = contentDescriptions;
    }

    public static Strand Create(
        string name,
        YearLevel yearLevel,
        List<Substrand>? substrands = null,
        List<ContentDescription>? contentDescriptions = null
    )
    {
        if (substrands is null && contentDescriptions is null)
        {
            throw new StrandCreationException();
        }

        return new Strand(name, yearLevel, substrands, contentDescriptions);
    }

    public List<ContentDescription> GetContentDescriptions()
    {
        if (_substrands is null)
        {
            return _contentDescriptions!;
        }

        var contentDescriptions = _substrands!
            .SelectMany(substrand => substrand.ContentDescriptions)
            .ToList();

        return contentDescriptions;
    }

    public void AddSubstrand(Substrand substrand)
    {
        _substrands!.Add(substrand);
    }

    public void AddContentDescriptions(List<ContentDescription> contentDescriptions)
    {
        foreach (var contentDescription in contentDescriptions)
        {
            AddContentDescription(contentDescription);
        }
    }

    public void AddContentDescription(ContentDescription contentDescription)
    {
        if (HasSubstrands)
        {
            throw new StrandHasSubstrandsException();
        }

        if (!_contentDescriptions!.Contains(contentDescription))
        {
            _contentDescriptions!.Add(contentDescription);
        }
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return _substrands?.AsReadOnly();
        yield return _contentDescriptions?.AsReadOnly();

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Strand() { }
}
