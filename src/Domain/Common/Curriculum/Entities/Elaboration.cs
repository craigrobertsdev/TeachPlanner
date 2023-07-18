using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;

namespace Domain.Common.Curriculum.Entities;

public sealed class Elaboration : Entity<ElaborationId>
{
    public string Description { get; private set; }

    private Elaboration(string description)
    {
        Description = description;
    }

    public static Elaboration Create(string description)
    {
        return new(description);
    }
}
