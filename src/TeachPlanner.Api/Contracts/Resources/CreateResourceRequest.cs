namespace TeachPlanner.Api.Contracts.Resources;

public record CreateResourceRequest(string Name, Guid SubjectId, List<string> AssociatedStrands, bool IsAssessment);