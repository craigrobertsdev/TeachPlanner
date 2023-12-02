namespace TeachPlanner.Api.Contracts.Resources;

public record CreateResourceRequest(Stream FileStream, string Name, Guid SubjectId, bool IsAssessment);