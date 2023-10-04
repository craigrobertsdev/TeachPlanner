using TeachPlanner.Api.Domain.Assessments;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IAssessmentRepository : IRepository<Assessment>
{
    public Task<List<Assessment>> GetAssessmentsById(List<AssessmentId> assessmentIds, CancellationToken cancellationToken);
}
