using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.YearDataRecords;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<YearDataId>))]
public record YearDataId : IStronglyTypedId {
    public Guid Value { get; set; }

    public YearDataId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<YearDataId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new YearDataId(value), mappingHints) {
        }
    }
}