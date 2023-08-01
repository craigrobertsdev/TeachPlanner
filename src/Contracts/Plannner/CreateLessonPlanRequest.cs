namespace Contracts.Plannner;

public record CreateLessonPlanRequest(
    string SubjectId,
    string PlanningNotes,
    List<string>? ResourceIds,
    List<string>? SummativeAssessmentIds,
    List<string>? FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime)
{
    public List<string> ResourceIds { get; init; } = ResourceIds ?? new();
    public List<string> SummativeAssessmentIds { get; init; } = SummativeAssessmentIds ?? new();
    public List<string> FormativeAssessmentIds { get; init; } = FormativeAssessmentIds ?? new();
}
