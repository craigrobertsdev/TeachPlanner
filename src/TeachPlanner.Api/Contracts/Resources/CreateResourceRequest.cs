using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Contracts.Resources;

public record CreateResourceRequest(string Name, SubjectId SubjectId, List<string> AssociatedStrands, bool IsAssessment);