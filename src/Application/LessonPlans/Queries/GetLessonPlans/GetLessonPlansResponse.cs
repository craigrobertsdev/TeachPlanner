using Domain.LessonPlanAggregate;

namespace Application.LessonPlans.Queries.GetLessonPlans;

public record GetLessonPlansResponse(
    List<LessonPlan> LessonPlans
    );
