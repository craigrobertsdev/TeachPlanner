using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.Entities;

public sealed class Substrand : Entity<SubstrandId>
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
}
