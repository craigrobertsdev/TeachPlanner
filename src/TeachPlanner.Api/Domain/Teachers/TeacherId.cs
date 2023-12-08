using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.Teachers;

public record TeacherId {
    public Guid Value;

    public TeacherId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<TeacherId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new TeacherId(value), mappingHints) {
        }
    }
}