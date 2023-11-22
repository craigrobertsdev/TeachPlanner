using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Services;

public class TermDateService : ITermDateService
{
    private readonly List<TermDate> _termDates = new();
    public IReadOnlyList<TermDate> TermDates => _termDates.AsReadOnly();
    public void SetTermDates(List<TermDate> termDates)
    {
        _termDates.Clear();
        _termDates.AddRange(termDates);
    }
}
