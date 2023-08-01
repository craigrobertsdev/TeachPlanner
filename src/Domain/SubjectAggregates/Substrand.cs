using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class Substrand : ValueObject
{
    private readonly List<ContentDescriptor> _contentDescriptors = new();
    public string Name { get; private set; }
    public IReadOnlyList<ContentDescriptor> ContentDescriptors => _contentDescriptors.AsReadOnly();

    private Substrand(string name, List<ContentDescriptor> contentDescriptors)
    {
        Name = name;
        _contentDescriptors = contentDescriptors;
    }

    public static Substrand Create(string name, List<ContentDescriptor> contentDescriptors)
    {
        return new(name, contentDescriptors);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return ContentDescriptors;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Substrand() { }
}
