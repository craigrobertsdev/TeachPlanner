using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlanners.Queries.GetLessonPlans;

public record GetLessonPlansResponse(
    List<LessonPlan> LessonPlans
    );
