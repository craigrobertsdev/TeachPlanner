﻿using Domain.Common.Primatives;
using Domain.LessonPlanAggregate;
using Domain.SubjectAggregates;

namespace Domain.ResourceAggregate;

public sealed class Resource : AggregateRoot<ResourceId>
{
    private readonly List<LessonPlan> _lessonPlans = new();
    public string Name { get; private set; }
    public string Url { get; private set; }
    public bool IsAssessment { get; private set; }
    public IReadOnlyList<LessonPlan> LessonPlan => _lessonPlans.AsReadOnly();
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
        string name,
        string url,
        bool isAssessment,
        SubjectId subjectId,
        List<string>? strandNames)
    {
        return new(new ResourceId(Guid.NewGuid()), name, url, isAssessment, subjectId, strandNames);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Resource() { }

}
