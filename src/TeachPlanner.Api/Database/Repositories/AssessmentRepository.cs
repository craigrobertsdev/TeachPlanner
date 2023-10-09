using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Assessments;

namespace TeachPlanner.Api.Database.Repositories;

public class AssessmentRepository : IAssessmentRepository
{
    private readonly ApplicationDbContext _context;

    public AssessmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Assessment>> GetAssessmentsById(List<AssessmentId> assessmentIds, CancellationToken cancellationToken)
    {
        return await _context.Assessments
                    .Where(x => assessmentIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);
    }
}
