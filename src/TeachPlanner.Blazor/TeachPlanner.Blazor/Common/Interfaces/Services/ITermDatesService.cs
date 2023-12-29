using TeachPlanner.Shared.Domain.PlannerTemplates;

namespace TeachPlanner.Blazor.Common.Interfaces.Services;

public interface ITermDatesService {
    public IReadOnlyList<TermDate> TermDates { get; }
    public void SetTermDates(List<TermDate> termDates);
    public DateOnly GetWeekStart(int termNumber, int weekNumber);
}
