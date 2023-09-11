namespace TeachPlanner.Contracts.LessonPlannner.CreateLessonPlan;

public record CreateLessonPlanRequest(
    string SubjectId,
    string PlanningNotes,
    List<ResourceRequest>? Resources,
    List<string>? SummativeAssessmentIds,
    List<string>? FormativeAssessmentIds,
    DateTime StartTime,
    DateTime EndTime)
{
    public List<ResourceRequest> Resources { get; init; } = Resources ?? new();
    public List<string> SummativeAssessmentIds { get; init; } = SummativeAssessmentIds ?? new();
    public List<string> FormativeAssessmentIds { get; init; } = FormativeAssessmentIds ?? new();
}

public record ResourceRequest(
    string Name,
    string Url,
    bool IsAssessment,
    string SubjectId,
    List<string>? AssociatedStrands);
