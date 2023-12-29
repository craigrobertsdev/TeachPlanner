using TeachPlanner.Shared.Contracts.Assessments;
using TeachPlanner.Shared.Contracts.Resources;

namespace TeachPlanner.Shared.Contracts.LessonPlans;

public record LessonPlanResponse(
    Guid LessonPlanId,
    Guid SubjectId,
    string PlanningNotes,
    List<ResourceResponse> Resources,
    List<AssessmentResponse> Assessments,
    List<LessonCommentResponse> Comments,
    DateTime StartTime,
    DateTime EndTime,
    int NumberOfPeriods);

public record LessonCommentResponse(
    string Content,
    bool Completed,
    bool StruckOut,
    DateTime? CompletedDateTime);