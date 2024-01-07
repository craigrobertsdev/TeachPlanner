using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Database;

namespace TeachPlanner.Shared.Database.Repositories;

public class TermPlannerRepository : ITermPlannerRepository {
    private readonly ApplicationDbContext _context;

    public TermPlannerRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<TermPlanner?> GetById(TermPlannerId id, CancellationToken cancellationToken) {
        return await _context.TermPlanners.FirstOrDefaultAsync(tp => tp.Id == id, cancellationToken);
    }

    public async Task<TermPlanner?> GetByYearDataIdAndYear(YearDataId yearDataId, int calendarYear,
        CancellationToken cancellationToken) {
        var termPlanner = await _context.TermPlanners
            .AsNoTracking()
            .Where(yd => yd.YearDataId == yearDataId)
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(tp => tp.TermPlans)
            .FirstOrDefaultAsync(cancellationToken);

        if (termPlanner is null) throw new TermPlannerNotFoundException();

        var subjectIds = termPlanner.TermPlans
            .Select(tp => tp.Subjects)
            .SelectMany(sl => sl.Select(s => s.Id))
            .ToList();

        var subjects = await _context.CurriculumSubjects
            .Where(s => subjectIds.Contains(s.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        termPlanner.PopulateSubjectsForTerms(subjects);

        return termPlanner;
    }

    public void Add(TermPlanner termPlanner) {
        _context.TermPlanners.Add(termPlanner);
    }

    public async Task Delete(TermPlannerId id, CancellationToken cancellationToken) {
        var termPlanner = await GetById(id, cancellationToken);

        if (termPlanner == null) return;

        _context.TermPlanners.Remove(termPlanner);
    }
}