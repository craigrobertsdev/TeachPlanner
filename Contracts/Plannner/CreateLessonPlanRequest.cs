namespace Contracts.Plannner;

public record CreateLessonPlanRequest(
    string SubjectId,
    string PlanningNotes,
    List<string>? ResourceIds,
    List<string>? AssessmentIds,
    DateTime StartTime,
    DateTime EndTime)
{
    public List<string> ResourceIds { get; init; } = ResourceIds ?? new();
    public List<string> AssessmentIds { get; init; } = AssessmentIds ?? new();
}
