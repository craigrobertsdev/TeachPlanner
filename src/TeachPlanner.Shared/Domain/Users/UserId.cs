using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Shared.Domain.Users;

public record UserId {
    public Guid Value;

    public UserId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<UserId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new UserId(value), mappingHints) {
        }
    }

    public class IdToStringConverter : ValueConverter<UserId, string> {
        public IdToStringConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value.ToString(), value => new UserId(Guid.Parse(value)), mappingHints) {
        }
    }
}