using Domain.Common.Primatives;
using Domain.Common.Curriculum;
using Domain.Common.Curriculum.ValueObjects;

namespace Domain.SubjectAggregates.Mathematics;

public sealed class MathematicsSubject : BaseSubject
{
    private MathematicsSubject(
        SubjectId id,
        string name,
        DateTime createdDateTime,
        DateTime updatedDateTime
    )
        : base(id, name, createdDateTime, updatedDateTime) { }

    public static MathematicsSubject Create(string name)
    {
        return new(SubjectId.Create(), name, DateTime.UtcNow, DateTime.UtcNow);
    }
}
