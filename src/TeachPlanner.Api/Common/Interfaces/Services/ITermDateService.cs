using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Common.Interfaces.Services;

public interface ITermDateService
{
    public IReadOnlyList<TermDate> TermDates { get; }
    public void SetTermDates(List<TermDate> termDates);
}
