using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.WeekPlanners;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<WeekPlannerId>))]
public record WeekPlannerId : IStronglyTypedId {
    public Guid Value { get; set; }

    public WeekPlannerId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<WeekPlannerId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new WeekPlannerId(value), mappingHints) {
        }
    }
}