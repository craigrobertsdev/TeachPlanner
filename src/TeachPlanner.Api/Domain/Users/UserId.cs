using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.Users;

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
}