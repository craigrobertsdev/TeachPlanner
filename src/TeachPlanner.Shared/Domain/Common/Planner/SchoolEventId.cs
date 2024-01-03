using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Common.Planner;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<SchoolEventId>))]
public record SchoolEventId : IStronglyTypedId {
    public Guid Value { get; set; }

    public SchoolEventId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<SchoolEventId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new SchoolEventId(value), mappingHints) {
        }
    }
}