using TeachPlanner.Api.Entities.Assessments;

namespace TeachPlanner.Api.Contracts.LessonPlannner.CreateLessonPlan;

public record CreateLessonPlanResponse(Guid Id);

public record ResourceResponse(Guid Id);

public record AssessmentResponse(Guid Id);

public record CommentResponse(
    string Content,
    bool Completed,
    bool StruckThrough,
    DateTime CreatedDateTime,
    DateTime UpdatedDateTime,
    DateTime? CompletedDateTime);
