namespace Contracts.Plannner;

public record CreateLessonPlanRequest(
    string SubjectId,
    string PlanningNotes,
    List<CreateResourceRequest>? Resources,
    List<CreateAssessmentRequest>? Assessments,
    DateTime StartTime,
    DateTime EndTime);

public record CreateResourceRequest(
    string Name,
    string Url);

public record CreateAssessmentRequest(
    string Name,
    string Url);

