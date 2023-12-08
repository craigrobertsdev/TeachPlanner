using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.CurriculumSubjects;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class SubjectQueries {
    public static async Task<List<CurriculumSubject>> GetCurriculumSubjects(
        this ApplicationDbContext context,
        bool includeElaborations,
        CancellationToken cancellationToken) {
        if (includeElaborations)
            return await GetSubjectsWithElaborations(context, new List<SubjectId>(), cancellationToken);

        return await GetSubjectsWithoutElaborations(context, new List<SubjectId>(), cancellationToken);
    }

    public static async Task<List<CurriculumSubject>?> GetSubjectsById(
        this ApplicationDbContext context,
        List<SubjectId> subjects,
        bool includeElaborations,
        CancellationToken cancellationToken) {
        if (includeElaborations) return await context.GetSubjectsWithElaborations(subjects, cancellationToken);

        return await context.GetSubjectsWithoutElaborations(subjects, cancellationToken);
    }

    private static async Task<List<CurriculumSubject>> GetSubjectsWithElaborations(
        this ApplicationDbContext context,
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<CurriculumSubject, bool>>? filter = null) {
        var subjectsQuery = context.CurriculumSubjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id));

        if (filter != null) subjectsQuery = subjectsQuery.Where(filter);

        subjectsQuery = subjectsQuery
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions)
            .ThenInclude(cd => cd.Elaborations);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);

        if (subjects.Count == 0) throw new NoSubjectsFoundException();

        return subjects;
    }

    private static async Task<List<CurriculumSubject>> GetSubjectsWithoutElaborations(
        this ApplicationDbContext context,
        List<SubjectId> subjectIds,
        CancellationToken cancellationToken,
        Expression<Func<CurriculumSubject, bool>>? filter = null) {
        var subjectsQuery = context.CurriculumSubjects
            .AsNoTracking()
            .Where(s => subjectIds.Contains(s.Id));

        if (filter != null) subjectsQuery = subjectsQuery.Where(filter);

        subjectsQuery = subjectsQuery
            .Include(s => s.YearLevels)
            .ThenInclude(yl => yl.Strands)
            .ThenInclude(s => s.ContentDescriptions);

        var subjects = await subjectsQuery.ToListAsync(cancellationToken);

        if (subjects.Count == 0) throw new NoSubjectsFoundException();

        return subjects;
    }
}