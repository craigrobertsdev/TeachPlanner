namespace TeachPlanner.Shared.Contracts.Resources;

public record CreateResourceRequest(Stream FileStream, string Name, Guid SubjectId, bool IsAssessment, List<string> AssociatedStrands);