using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Extensions;

public static class TermPlannerQueries
{
    public async static Task<TermPlanner> GetTermPlanner(this ApplicationDbContext context,
        YearDataId yearDataId, int calendarYear, CancellationToken cancellationToken)
    {
        var termPlanner = await context.TermPlanners
            .AsNoTracking()
            .Where(yd => yd.YearDataId == yearDataId)
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(tp => tp.TermPlans)
            .FirstOrDefaultAsync(cancellationToken);

        if (termPlanner is null)
        {
            throw new TermPlannerNotFoundException();
        }

        var subjectIds = termPlanner.TermPlans
            .Select(tp => tp.Subjects)
            .SelectMany(sl => sl.Select(s => s.Id))
            .ToList();

        var subjects = await context.Subjects
            .Where(s => subjectIds.Contains(s.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        termPlanner.PopulateSubjectsForTerms(subjects);

        return termPlanner;
    }
}
