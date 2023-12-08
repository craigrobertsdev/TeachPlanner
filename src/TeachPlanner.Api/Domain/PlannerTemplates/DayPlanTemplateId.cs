using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record DayPlanTemplateId {
    public DayPlanTemplateId(Guid value) {
        Value = value;
    }

    public Guid Value { get; }

    public class StronglyTypedIdEfValueConverter : ValueConverter<DayPlanTemplateId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new DayPlanTemplateId(value), mappingHints) {
        }
    }
}
