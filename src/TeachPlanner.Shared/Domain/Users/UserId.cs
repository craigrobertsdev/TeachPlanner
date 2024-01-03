using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Users;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<UserId>))]
public record UserId : IStronglyTypedId {
    public Guid Value { get; set; }

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