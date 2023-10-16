using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Resources.DomainEvents;

namespace TeachPlanner.Api.Domain.Resources;

public sealed class Resource : Entity<ResourceId>, IAggregateRoot
{
    private readonly List<LessonPlanResource> _lessonPlanResources = new();
    public string Name { get; private set; }
    public string Url { get; private set; }
    public bool IsAssessment { get; private set; }
    public IReadOnlyList<LessonPlanResource> LessonPlanResources => _lessonPlanResources.AsReadOnly();
    public SubjectId SubjectId { get; private set; }
    public List<string> AssociatedStrands { get; private set; } = new();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Resource(
        ResourceId id,
        string name,
        string url,
        bool isAssessment,
        SubjectId subjectId,
        List<string>? associatedStrands = null) : base(id)
    {
        Name = name;
        Url = url;
        IsAssessment = isAssessment;
        SubjectId = subjectId;

        if (associatedStrands is not null)
        {
            AssociatedStrands = associatedStrands;
        }

        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static Resource Create(
        TeacherId teacherId,
        string name,
        string url,
        bool isAssessment,
        SubjectId subjectId,
        List<string>? strandNames)
    {
        var resource = new Resource(
            new ResourceId(Guid.NewGuid()),
            name,
            url,
            isAssessment,
            subjectId,
            strandNames);

        resource.AddDomainEvent(new ResourceCreatedDomainEvent(resource.Id, teacherId));

        return resource;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Resource() { }

}
