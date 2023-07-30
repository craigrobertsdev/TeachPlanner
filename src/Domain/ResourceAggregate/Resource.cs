using Domain.Common.Primatives;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.ResourceAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.ResourceAggregate;

public sealed class Resource : AggregateRoot<ResourceId>
{
    //private readonly List<LessonPlanId> _lessonPlanIds = new();
    public string Name { get; private set; }
    public string Url { get; private set; }
    public bool IsAssessment { get; private set; }
    //public IReadOnlyList<LessonPlanId> LessonPlanIds => _lessonPlanIds.AsReadOnly();
    public SubjectIdForReference SubjectId { get; private set; }
    public StrandIdForReference? StrandId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Resource(
        ResourceId id,
        string name,
        string url,
        bool isAssessment,
        SubjectIdForReference subjectId,
        StrandIdForReference? strandId) : base(id)
    {
        Name = name;
        Url = url;
        IsAssessment = isAssessment;
        SubjectId = subjectId;
        StrandId = strandId;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static Resource Create(
        string name,
        string url,
        bool isAssessment,
        SubjectIdForReference subjectId,
        StrandIdForReference? strandId)
    {
        return new(ResourceId.Create(), name, url, isAssessment, subjectId, strandId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Resource() { }

}
