using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.Repositories;
public class CurriculumRepository : ICurriculumRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCurriculum(List<Subject> subjects, CancellationToken cancellationToken)
    {
        foreach (var subject in subjects)
        {
            if (!subject.IsCurriculumSubject)
            {
                throw new AttemptedToAddNonCurriculumSubjectException(subject.Name);
            }
        }

        // clear existing curriculum subjects
        var curriculumSubjects = await _context.Subjects
            .Where(s => s.IsCurriculumSubject)
            .ToListAsync(cancellationToken);

        _context.Subjects.RemoveRange(curriculumSubjects);
        await _context.SaveChangesAsync(cancellationToken);

        // add new subjects
        foreach (var subject in subjects)
        {
            _context.Subjects.Add(subject);
        }
    }
}
