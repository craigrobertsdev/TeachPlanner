namespace TeachPlanner.Shared.Contracts.Subjects;

public record SubjectRequest(string Name, List<string> ContentDescriptionIds);