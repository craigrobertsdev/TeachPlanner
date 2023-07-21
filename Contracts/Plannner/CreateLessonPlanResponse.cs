namespace Contracts.Plannner;

public record CreateLessonPlanResponse(
    string Id,
    string SubjectId,
    string PlanningNotes,
    List<ResourceResponse> Resources,
    List<AssessmentResponse> Assessments,
    List<CommentResponse> Comments,
    DateTime StartTime,
    DateTime EndTime);

public record ResourceResponse(
    string Id,
    string Name,
    string Url);

public record AssessmentResponse(
    string Id,
    string Name,
    string Url);

public record CommentResponse(
    string Id,
    string Content,
    bool Completed,
    bool StruckThrough,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime,
    DateTime? CompletedDateTime);
