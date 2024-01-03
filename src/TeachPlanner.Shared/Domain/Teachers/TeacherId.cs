using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Teachers;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<TeacherId>))]
public record TeacherId : IStronglyTypedId {
    public Guid Value { get; set; }

    public TeacherId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<TeacherId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new TeacherId(value), mappingHints) {
        }
    }
}