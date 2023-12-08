using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Domain.LessonPlans;

public record LessonPlanResource {
    public LessonPlanResource(LessonPlanId lessonPlanId, ResourceId resourceId) {
        LessonPlanId = lessonPlanId;
        ResourceId = resourceId;
    }

    public LessonPlanId LessonPlanId { get; private set; }
    public ResourceId ResourceId { get; private set; }

    public static LessonPlanResource Create(LessonPlanId lessonPlanId, ResourceId resourceId) {
        return new LessonPlanResource(lessonPlanId, resourceId);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlanResource() {
    }
}