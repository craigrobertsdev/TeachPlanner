using Domain.Common.Primatives;
using OneOf;

namespace Domain.SubjectAggregates;

public sealed class Strand : ValueObject
{
    public string Name { get; private set; }

    // one of these properties will be null depending on the subject
    private readonly List<Substrand>? _substrands = new();
    private readonly List<ContentDescriptor>? _contentDescriptors = new();

    private Strand(
        string name,
        List<Substrand>? substrands = null,
        List<ContentDescriptor>? contentDescriptors = null
    )
    {
        Name = name;
        _substrands = substrands;
        _contentDescriptors = contentDescriptors;
    }

    /// <summary>
    /// This method is used to create a Strand entity. It will return an error if both substrands and contentDescriptors are null.
    /// Some subjects have substrands, others only have content descriptors.
    /// Exactly one of substrands or contentDescriptors must be provided.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="substrands"></param>
    /// <param name="contentDescriptors"></param>
    /// <returns></returns>
    public static OneOf<Strand, ArgumentException> Create(
        string name,
        List<Substrand>? substrands = null,
        List<ContentDescriptor>? contentDescriptors = null
    )
    {
        if (substrands is null && contentDescriptors is null)
        {
            return new ArgumentException(
                "Either substrands or contentDescriptors must be provided"
            );
        }

        return new Strand(name, substrands, contentDescriptors);
    }

    public List<ContentDescriptor> GetContentDescriptors()
    {
        if (_substrands is null)
        {
            return _contentDescriptors!;
        }

        var contentDescriptors = _substrands!
            .SelectMany(substrand => substrand.ContentDescriptors)
            .ToList();

        return contentDescriptors;
    }

    public void AddSubstrand(Substrand substrand)
    {
        _substrands!.Add(substrand);
    }

    public void AddContentDescriptor(ContentDescriptor contentDescriptor)
    {
        _contentDescriptors!.Add(contentDescriptor);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return _substrands?.AsReadOnly();
        yield return _contentDescriptors?.AsReadOnly();

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Strand() { }
}
