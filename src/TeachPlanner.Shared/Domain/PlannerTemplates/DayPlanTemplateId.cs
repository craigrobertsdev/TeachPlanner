using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.PlannerTemplates;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<DayPlanTemplateId>))]
public record DayPlanTemplateId : IStronglyTypedId {
    public Guid Value { get; set; }

    public DayPlanTemplateId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<DayPlanTemplateId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new DayPlanTemplateId(value), mappingHints) {
        }
    }
}
