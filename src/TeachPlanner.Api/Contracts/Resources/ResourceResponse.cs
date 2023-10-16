namespace TeachPlanner.Api.Contracts.Resources;

public record ResourceResponse(
    string Name,
    string Url,
    bool IsAssessment);
