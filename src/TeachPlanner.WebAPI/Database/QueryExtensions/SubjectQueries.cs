using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class SubjectQueries
{
    public static async Task<List<Subject>> GetCurriculumSubjects(
        this ApplicationDbContext context,
        bool includeElaborations,
        CancellationToken cancellationToken)
    {
        Expression<Func<Subject, bool>> filter = s => s.IsCurriculumSubject;

        if (includeElaborations)
        {
            return await GetSubjectsWithElaborations(context, new List<SubjectId>(), cancellationToken, filter);
        }

        return await GetSubjectsWithoutElaborations(context, new List<SubjectId>(), cancellationToken, filter);
    }

    public static async Task<List<Subject>?> GetSubjectsById(
        this ApplicationDbContext context,
        List<SubjectId> subjects,
        bool includeElaborations,
        CancellationToken cancellationToken)
    {
        if (includeElaborations)
        {
            return await context.GetSubjectsWithElaborations(subjects, cancellationToken);
        }

        return await context.GetSubjectsWithoutElaborations(subjects, cancellationToken);
    }

    private static async Task<List<Subject>> GetSubjectsWithElaborations(
        this ApplicationDbContext context,
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id));

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

    private static async Task<List<Subject>> GetSubjectsWithoutElaborations(
        this ApplicationDbContext context,
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<Subject, bool>>? filter = null)
    {
        var subjectsQuery = context.Subjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id));

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
