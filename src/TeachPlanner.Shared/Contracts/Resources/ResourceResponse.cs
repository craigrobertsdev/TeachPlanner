namespace TeachPlanner.Shared.Contracts.Resources;

public record ResourceResponse(
    string Name,
    string Url,
    bool IsAssessment);