using Domain.Common.Primatives;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.SubjectAggregates.Entities;

public sealed class ContentDescriptor : Entity<ContentDescriptorId>
{
    private readonly List<Elaboration> _elaborations = new();
    public string Description { get; private set; }
    public IReadOnlyList<Elaboration> Elaborations => _elaborations.AsReadOnly();

    private ContentDescriptor(ContentDescriptorId id, string description, List<Elaboration> elaborations) : base(id)
    {
        Description = description;
        _elaborations = elaborations;
    }

    public static ContentDescriptor Create(string description, List<Elaboration> elaborations)
    {
        return new(ContentDescriptorId.Create(), description, elaborations);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContentDescriptor() { }
}
