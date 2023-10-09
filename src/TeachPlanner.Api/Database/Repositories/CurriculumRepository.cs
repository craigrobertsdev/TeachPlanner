using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Database.Repositories;
public class CurriculumRepository : ICurriculumRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCurriculum(List<CurriculumSubject> subjects, CancellationToken cancellationToken)
    {
        // clear existing curriculum subjects
        var curriculumSubjects = await _context.CurriculumSubjects
            .ToListAsync(cancellationToken);

        _context.CurriculumSubjects.RemoveRange(curriculumSubjects);
        await _context.SaveChangesAsync(cancellationToken);

        // add new subjects
        foreach (var subject in subjects)
        {
            _context.CurriculumSubjects.Add(subject);
        }
    }
}
