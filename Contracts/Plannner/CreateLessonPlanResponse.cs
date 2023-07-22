using System;

namespace Contracts.Plannner;

public record CreateLessonPlanResponse(
    string Id,
    string SubjectId,
    string PlanningNotes,
    List<ResourceResponse> ResourceIds,
    List<AssessmentResponse> AssessmentIds,
    DateTime StartTime,
    DateTime EndTime);

public record ResourceResponse(string Id);

public record AssessmentResponse(string Id);

public record CommentResponse(
    string Id,
    string Content,
    bool Completed,
    bool StruckThrough,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime,
    DateTime? CompletedDateTime);
