using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Teachers;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<ResourceId>))]
public record ResourceId : IStronglyTypedId {
    public Guid Value { get; set; }

    public ResourceId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<ResourceId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new ResourceId(value), mappingHints) {
        }
    }
}