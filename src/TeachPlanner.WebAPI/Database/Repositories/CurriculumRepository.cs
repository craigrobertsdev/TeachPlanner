using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Entities.Subjects;

namespace TeachPlanner.Api.Database.Repositories;
public class CurriculumRepository : ICurriculumRepository
{
    private readonly ApplicationDbContext _context;

    public CurriculumRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetSubjects(bool elaborations, CancellationToken cancellationToken)
    {
        IQueryable<Subject> query;
        IQueryable<Subject> mathsQuery;

        if (elaborations == true)
        {
            query = _context.Subjects
                .Include(s => s.YearLevels)
                .ThenInclude(yl => yl.Strands)
                .ThenInclude(s => s.Substrands!)
                .ThenInclude(s => s.ContentDescriptions)
                .ThenInclude(cd => cd.Elaborations)
                .Where(s => s.Name != "Mathematics");


            mathsQuery = _context.Subjects
                .Include(s => s.YearLevels)
                .ThenInclude(yl => yl.Strands)
                .ThenInclude(s => s.ContentDescriptions!)
                .ThenInclude(cd => cd.Elaborations)
                .Where(s => s.Name == "Mathematics");
        }
        else
        {
            query = _context.Subjects
                .Include(s => s.YearLevels)
                .ThenInclude(yl => yl.Strands)
                .ThenInclude(s => s.Substrands!)
                .ThenInclude(s => s.ContentDescriptions)
                .Where(s => s.Name != "Mathematics");


            mathsQuery = _context.Subjects
                .Include(s => s.YearLevels)
                .ThenInclude(yl => yl.Strands)
                .ThenInclude(s => s.ContentDescriptions!)
                .Where(s => s.Name == "Mathematics");
        }

        var subjects = await query.AsNoTracking().ToListAsync();
        var maths = await mathsQuery.AsNoTracking().SingleOrDefaultAsync();

        if (maths != null)
        {
            subjects.Add(maths);
        }

        return subjects;
    }

    public async Task<List<Subject>> GetCurriculumSubjectNamesAndIds(CancellationToken cancellationToken)
    {
        return await _context.Subjects
            .Where(s => s.IsCurriculumSubject)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveCurriculum(List<Subject> subjects, CancellationToken cancellationToken)
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
