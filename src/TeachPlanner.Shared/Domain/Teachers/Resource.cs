using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.LessonPlans;

namespace TeachPlanner.Shared.Domain.Teachers;

public sealed class Resource : Entity<ResourceId> {
    private readonly List<LessonPlanResource> _lessonPlanResources = new();

    private Resource(
        ResourceId id,
        string name,
        string url,
        bool isAssessment,
        SubjectId subjectId,
        List<string>? associatedStrands = null) : base(id) {
        Name = name;
        Url = url;
        IsAssessment = isAssessment;
        SubjectId = subjectId;

        if (associatedStrands is not null) AssociatedStrands = associatedStrands;

        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public string Name { get; private set; }
    public string Url { get; private set; }
    public bool IsAssessment { get; private set; }
    public IReadOnlyList<LessonPlanResource> LessonPlanResources => _lessonPlanResources.AsReadOnly();
    public SubjectId SubjectId { get; private set; }
    public List<string> AssociatedStrands { get; private set; } = new();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static Resource Create(
        TeacherId teacherId,
        string name,
        string url,
        bool isAssessment,
        SubjectId subjectId,
        List<string>? strandNames) {
        var resource = new Resource(
            new ResourceId(Guid.NewGuid()),
            name,
            url,
            isAssessment,
            subjectId,
            strandNames);

        return resource;
    }
#pragma warning disable CS8618 // non-nullable field must contain a non-null value when exiting constructor. consider declaring as nullable.
    private Resource() {
    }
}