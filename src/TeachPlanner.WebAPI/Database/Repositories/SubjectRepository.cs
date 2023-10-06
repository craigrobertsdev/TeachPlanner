using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.Repositories;
public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subject>> GetCurriculumSubjects(
        bool includeElaborations,
        CancellationToken cancellationToken)
    {
        Expression<Func<Subject, bool>> filter = s => s.IsCurriculumSubject;

        if (includeElaborations)
        {
            return await GetSubjectsWithElaborations(new List<SubjectId>(), cancellationToken, filter);
        }

        return await GetSubjectsWithoutElaborations(new List<SubjectId>(), cancellationToken, filter);
    }
    public async Task<List<Subject>> GetCurriculumSubjectNamesAndIds(CancellationToken cancellationToken)
    {
        return await _context.Subjects
            .Where(s => s.IsCurriculumSubject)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Subject>> GetSubjectsById(
        List<SubjectId> subjects,
        bool includeElaborations,
        CancellationToken cancellationToken)
    {
        Expression<Func<Subject, bool>> filter = s => subjects.Contains(s.Id);

        if (includeElaborations)
        {
            return await GetSubjectsWithElaborations(subjects, cancellationToken, filter);
        }

        return await GetSubjectsWithoutElaborations(subjects, cancellationToken, filter);
    }

    private async Task<List<Subject>> GetSubjectsWithElaborations(
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => s.Name != "Mathematics");

        var mathsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => s.Name == "Mathematics");

        if (filter != null)
        {
            subjectsQuery = subjectsQuery.Where(filter);
            mathsQuery = mathsQuery.Where(filter);
        }

        subjectsQuery = subjectsQuery
        .Include(s => s.YearLevels)
        .ThenInclude(yl => yl.Strands)
        .ThenInclude(s => s.ContentDescriptions)
        .ThenInclude(cd => cd.Elaborations);

        mathsQuery.Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!)
            .ThenInclude(cd => cd.Elaborations);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);
        var maths = await mathsQuery.SingleOrDefaultAsync(cancellationToken);

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            throw new NoSubjectsFoundException();
        }

        return subjects;
    }

    private async Task<List<Subject>> GetSubjectsWithoutElaborations(
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => s.Name != "Mathematics");

        var mathsQuery = _context.Subjects
            .AsNoTracking()
            .Where(s => s.Name == "Mathematics");

        if (filter != null)
        {
            subjectsQuery = subjectsQuery.Where(filter);
            mathsQuery = mathsQuery.Where(filter);
        }

        subjectsQuery = subjectsQuery
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions);

        mathsQuery.Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions!);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);
        var maths = await mathsQuery.FirstOrDefaultAsync(cancellationToken);

        if (maths != null)
        {
            subjects.Add(maths);
        }

        if (subjects.Count == 0)
        {
            throw new NoSubjectsFoundException();
        }

        return subjects;
    }
}
