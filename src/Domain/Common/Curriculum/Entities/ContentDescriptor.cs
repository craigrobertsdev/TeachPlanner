using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.Entities;

public sealed class ContentDescriptor : Entity<ContentDescriptorId>
{
    private readonly List<Elaboration> _elaborations = new();
    public string Description { get; private set; }
    public IReadOnlyList<Elaboration> Elaborations => _elaborations.AsReadOnly();

    private ContentDescriptor(string description, List<Elaboration> elaborations)
    {
        Description = description;
        _elaborations = elaborations;
    }

    public static ContentDescriptor Create(string description, List<Elaboration> elaborations)
    {
        return new(description, elaborations);
    }
}
