using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.LessonPlans;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<LessonPlanId>))]
public record LessonPlanId : IStronglyTypedId {
    public Guid Value { get; set; }

    public LessonPlanId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<LessonPlanId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new LessonPlanId(value), mappingHints) {
        }
    }
}