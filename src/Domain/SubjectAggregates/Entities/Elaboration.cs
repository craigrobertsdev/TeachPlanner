using Domain.Common.Primatives;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.SubjectAggregates.Entities;

public sealed class Elaboration : Entity<ElaborationId>
{
    public string Description { get; private set; }

    private Elaboration(ElaborationId id, string description) : base(id)
    {
        Description = description;
    }

    public static Elaboration Create(string description)
    {
        return new(new ElaborationId(Guid.NewGuid()), description);
    }
}
