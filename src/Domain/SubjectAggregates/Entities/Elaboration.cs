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
        return new(ElaborationId.Create(), description);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Elaboration() { }
}
