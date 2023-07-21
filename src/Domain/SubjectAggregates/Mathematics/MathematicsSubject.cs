using Domain.Common.Primatives;
using Domain.Common.Curriculum;
using Domain.Common.Curriculum.ValueObjects;

namespace Domain.SubjectAggregates.Mathematics;

public sealed class MathematicsSubject : Subject
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
        return new(new SubjectId(Guid.NewGuid()), name, DateTime.UtcNow, DateTime.UtcNow);
    }
}
