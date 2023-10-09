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
            return await GetSubjectsWithElaborations(cancellationToken, filter);
        }

        return await GetSubjectsWithoutElaborations(cancellationToken, filter);
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
            return await GetSubjectsWithElaborations(cancellationToken, filter);
        }

        return await GetSubjectsWithoutElaborations(cancellationToken, filter);
    }

    private async Task<List<Subject>> GetSubjectsWithElaborations(
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = _context.Subjects
            .AsNoTracking();

        if (filter != null)
        {
            subjectsQuery = subjectsQuery.Where(filter);
        }

        subjectsQuery = subjectsQuery
        .Include(s => s.YearLevels)
        .ThenInclude(yl => yl.Strands)
        .ThenInclude(s => s.ContentDescriptions)
        .ThenInclude(cd => cd.Elaborations);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);

        if (subjects.Count == 0)
        {
            throw new NoSubjectsFoundException();
        }

        return subjects;
    }

    private async Task<List<Subject>> GetSubjectsWithoutElaborations(
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = _context.Subjects
            .AsNoTracking();

        if (filter != null)
        {
            subjectsQuery = subjectsQuery.Where(filter);
        }

        subjectsQuery = subjectsQuery
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);

        if (subjects.Count == 0)
        {
            throw new NoSubjectsFoundException();
        }

        return subjects;
    }
}
