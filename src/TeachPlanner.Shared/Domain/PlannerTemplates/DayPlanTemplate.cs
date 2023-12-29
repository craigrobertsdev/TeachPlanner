using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Domain.PlannerTemplates;

public class DayPlanTemplate : Entity<DayPlanTemplateId> {
    private readonly List<TemplatePeriod> _periods = new();
    public TeacherId TeacherId { get; private set; }
    public IReadOnlyList<TemplatePeriod> Periods => _periods.AsReadOnly();
    public int NumberOfPeriods => Periods.Count;

    public void SetPeriods(IEnumerable<TemplatePeriod> periods) {
        _periods.Clear();
        _periods.AddRange(periods);
    }

    private DayPlanTemplate(DayPlanTemplateId id, List<TemplatePeriod> periods, TeacherId teacherId) : base(id) {
        _periods = periods;
        TeacherId = teacherId;
    }

    public static DayPlanTemplate Create(List<TemplatePeriod> periods, TeacherId teacherId) {
        return new DayPlanTemplate(new DayPlanTemplateId(Guid.NewGuid()), periods, teacherId);
    }

#pragma warning disable CS8618
    private DayPlanTemplate() { }
}