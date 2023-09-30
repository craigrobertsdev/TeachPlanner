using TeachPlanner.Api.Entities.LessonPlans;

namespace TeachPlanner.Api.Features.LessonPlanners.Queries.GetLessonPlans;

public record GetLessonPlansResponse(
    List<LessonPlan> LessonPlans
    );
