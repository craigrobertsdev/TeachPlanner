using Microsoft.EntityFrameworkCore;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Blazor.Database;
using TeachPlanner.Shared.Domain.Assessments;

namespace TeachPlanner.Blazor.Database.Repositories;

public class AssessmentRepository : IAssessmentRepository {
    private readonly ApplicationDbContext _context;

    public AssessmentRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<Assessment>> GetAssessmentsById(List<AssessmentId> assessmentIds,
        CancellationToken cancellationToken) {
        return await _context.Assessments
            .Where(x => assessmentIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}