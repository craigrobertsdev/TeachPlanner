using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.Entities;

public sealed class Substrand : Entity<SubstrandId>
{
    private readonly List<ContentDescriptor> _contentDescriptors = new();
    public string Name { get; private set; }
    public IReadOnlyList<ContentDescriptor> ContentDescriptors => _contentDescriptors.AsReadOnly();

    private Substrand(SubstrandId id, string name, List<ContentDescriptor> contentDescriptors) : base(id)
    {
        Name = name;
        _contentDescriptors = contentDescriptors;
    }

    public static Substrand Create(string name, List<ContentDescriptor> contentDescriptors)
    {
        return new(new SubstrandId(Guid.NewGuid()), name, contentDescriptors);
    }
}
