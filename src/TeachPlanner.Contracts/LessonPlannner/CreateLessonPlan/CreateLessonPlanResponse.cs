namespace TeachPlanner.Contracts.LessonPlannner.CreateLessonPlan;

public record CreateLessonPlanResponse(
    string Id,
    string SubjectId,
    string PlanningNotes,
    List<ResourceResponse> ResourceIds,
    List<SummativeAssessmentResponse> SummativeAssessmentIds,
    List<FormativeAssessmentResponse> FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime);

public record ResourceResponse(string Id);

public record SummativeAssessmentResponse(string Id);
public record FormativeAssessmentResponse(string Id);

public record CommentResponse(
    string Id,
    string Content,
    bool Completed,
    bool StruckThrough,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime,
    DateTime? CompletedDateTime);
