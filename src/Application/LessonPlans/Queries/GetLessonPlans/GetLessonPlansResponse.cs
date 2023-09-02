using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlans.Queries.GetLessonPlans;

public record GetLessonPlansResponse(
    List<LessonPlan> LessonPlans
    );
