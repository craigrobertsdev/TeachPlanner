using TeachPlanner.Api.Contracts.Assessments;
using TeachPlanner.Api.Contracts.Resources;

namespace TeachPlanner.Api.Contracts.LessonPlans;

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