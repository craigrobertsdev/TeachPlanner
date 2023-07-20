using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.Entities;

public sealed class Elaboration : Entity<ElaborationId>
{
    public string Description { get; private set; }

    private Elaboration(ElaborationId id, string description) : base(id)
    {
        Description = description;
    }

    public static Elaboration Create(string description)
    {
        return new(new ElaborationId(Guid.NewGuid(), description);
    }
}
