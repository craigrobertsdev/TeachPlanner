using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Shared.Domain.TermPlanners;

public record TermPlannerId {
    public Guid Value;

    public TermPlannerId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<TermPlannerId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new TermPlannerId(value), mappingHints) {
        }
    }
}