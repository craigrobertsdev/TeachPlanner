using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Database.Repositories;

public class SubjectRepository : ISubjectRepository {
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<CurriculumSubject>> GetCurriculumSubjects(
        bool includeElaborations,
        CancellationToken cancellationToken) {
        if (includeElaborations) return await GetSubjectsWithElaborations(cancellationToken);

        return await GetSubjectsWithoutElaborations(cancellationToken);
    }

    public async Task<List<CurriculumSubject>> GetSubjectsById(
        List<SubjectId> subjects,
        bool includeElaborations,
        CancellationToken cancellationToken) {
        Expression<Func<CurriculumSubject, bool>> filter = s => subjects.Contains(s.Id);

        if (includeElaborations) return await GetSubjectsWithElaborations(cancellationToken, filter);

        return await GetSubjectsWithoutElaborations(cancellationToken, filter);
    }

    private async Task<List<CurriculumSubject>> GetSubjectsWithElaborations(
        CancellationToken cancellationToken,
        Expression<Func<CurriculumSubject, bool>>? filter = null) {
        var subjectsQuery = _context.CurriculumSubjects
            .AsNoTracking();

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

    private async Task<List<CurriculumSubject>> GetSubjectsWithoutElaborations(
        CancellationToken cancellationToken,
        Expression<Func<CurriculumSubject, bool>>? filter = null) {
        var subjectsQuery = _context.CurriculumSubjects
            .AsNoTracking();

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