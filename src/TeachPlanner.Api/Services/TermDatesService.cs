using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Services;

public class TermDatesService : ITermDatesService
{
    private readonly List<TermDate> _termDates = new();
    private readonly IServiceProvider _serviceProvider;

    public IReadOnlyList<TermDate> TermDates => _termDates.AsReadOnly();

    public TermDatesService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _termDates = Task.Run(LoadTermDates).Result;
    }

    private async Task<List<TermDate>> LoadTermDates()
    {
        using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var termDates = await _context.TermDates.ToListAsync();
        return termDates;
    }
    public void SetTermDates(List<TermDate> termDates)
    {
        _termDates.Clear();
        _termDates.AddRange(termDates);
    }

    public DateOnly GetWeekStart(int termNumber, int weekNumber)
    {
        if (termNumber < 1 || termNumber > 4)
        {
            throw new ArgumentException("Term number must be between 1 and 4");
        }

        if (weekNumber < 0)
        {
            throw new ArgumentException("Week number must be greater than 0");
        }

        var term = _termDates.First(x => x.TermNumber == termNumber);

        var termStart = term.StartDate.AddDays(7 * (weekNumber - 1));

        return termStart;
    }
}
