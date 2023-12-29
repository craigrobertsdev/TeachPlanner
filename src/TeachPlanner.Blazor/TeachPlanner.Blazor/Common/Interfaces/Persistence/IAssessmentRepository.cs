using TeachPlanner.Shared.Domain.Assessments;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IAssessmentRepository : IRepository<Assessment> {
    public Task<List<Assessment>> GetAssessmentsById(List<AssessmentId> assessmentIds,
        CancellationToken cancellationToken);
}