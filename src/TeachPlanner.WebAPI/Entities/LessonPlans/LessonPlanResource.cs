using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.Resources;

namespace TeachPlanner.Api.Entities.LessonPlans;

public class LessonPlanResource : ValueObject
{
    public LessonPlanId LessonPlanId { get; private set; }
    public ResourceId ResourceId { get; private set; }

    public LessonPlanResource(LessonPlanId lessonPlanId, ResourceId resourceId)
    {
        LessonPlanId = lessonPlanId;
        ResourceId = resourceId;
    }

    public static LessonPlanResource Create(LessonPlanId lessonPlanId, ResourceId resourceId)
    {
        return new LessonPlanResource(lessonPlanId, resourceId);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return LessonPlanId;
        yield return ResourceId;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlanResource() { }
}
