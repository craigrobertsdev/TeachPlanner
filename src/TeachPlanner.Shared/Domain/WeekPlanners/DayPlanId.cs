using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.WeekPlanners;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<DayPlanId>))]
public record DayPlanId : IStronglyTypedId {
    public Guid Value { get; set; }

    public DayPlanId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<DayPlanId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new DayPlanId(value), mappingHints) {
        }
    }
}