using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Calendar;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<CalendarId>))]
public record CalendarId : IStronglyTypedId {
    public Guid Value { get; set; }

    public CalendarId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<CalendarId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new CalendarId(value), mappingHints) {
        }
    }
}