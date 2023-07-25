using Domain.Common.Primatives;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.SubjectAggregates.Entities;

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
