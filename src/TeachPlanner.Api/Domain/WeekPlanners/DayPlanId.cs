using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record DayPlanId {
    public DayPlanId(Guid value) {
        Value = value;
    }

    public Guid Value { get; init; }

    public class StronglyTypedIdEfValueConverter : ValueConverter<DayPlanId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new DayPlanId(value), mappingHints) {
        }
    }
}