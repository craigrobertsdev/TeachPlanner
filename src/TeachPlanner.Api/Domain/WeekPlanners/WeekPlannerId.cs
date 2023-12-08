using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record WeekPlannerId {
    public Guid Value;

    public WeekPlannerId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<WeekPlannerId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new WeekPlannerId(value), mappingHints) {
        }
    }
}