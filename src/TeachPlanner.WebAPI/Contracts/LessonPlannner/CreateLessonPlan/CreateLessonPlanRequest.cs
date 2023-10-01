using TeachPlanner.Api.Entities.LessonPlans;

namespace TeachPlanner.Api.Contracts.LessonPlannner.CreateLessonPlan;

public record CreateLessonPlanRequest(
    Guid TeacherId,
    Guid SubjectId,
    string PlanningNotes,
    List<LessonPlanResource>? LessonPlanResources,
    List<Guid>? AssessmentIds,
    DateTime StartTime,
    DateTime EndTime,
    int NumberOfPeriods);

