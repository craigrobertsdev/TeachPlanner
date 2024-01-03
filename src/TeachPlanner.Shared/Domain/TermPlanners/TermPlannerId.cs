using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.TermPlanners;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<TermPlannerId>))]
public record TermPlannerId : IStronglyTypedId {
    public Guid Value { get; set; }

    public TermPlannerId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<TermPlannerId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new TermPlannerId(value), mappingHints) {
        }
    }
}