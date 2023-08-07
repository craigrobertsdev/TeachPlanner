using Domain.Common.Primatives;
using OneOf;

namespace Domain.SubjectAggregates;

public sealed class Strand : ValueObject
{
    public string Name { get; private set; }

    // one of these properties will be null depending on the subject
    private readonly List<Substrand>? _substrands = new();
    private readonly List<ContentDescription>? _contentDescriptions = new();
    public IReadOnlyList<Substrand>? Substrands => _substrands?.AsReadOnly();
    public IReadOnlyList<ContentDescription>? ContentDescriptions => _contentDescriptions?.AsReadOnly();

    private Strand(
        string name,
        List<Substrand>? substrands = null,
        List<ContentDescription>? contentDescriptions = null
    )
    {
        Name = name;
        _substrands = substrands;
        _contentDescriptions = contentDescriptions;
    }

    /// <summary>
    /// This method is used to create a Strand entity. It will return an error if both substrands and contentDescriptions are null.
    /// Some subjects have substrands, others only have content descriptors.
    /// Exactly one of substrands or contentDescriptions must be provided.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="substrands"></param>
    /// <param name="contentDescriptions"></param>
    /// <returns></returns>
    public static OneOf<Strand, ArgumentException> Create(
        string name,
        List<Substrand>? substrands = null,
        List<ContentDescription>? contentDescriptions = null
    )
    {
        if (substrands is null && contentDescriptions is null)
        {
            return new ArgumentException(
                "Either substrands or contentDescriptions must be provided"
            );
        }

        return new Strand(name, substrands, contentDescriptions);
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
