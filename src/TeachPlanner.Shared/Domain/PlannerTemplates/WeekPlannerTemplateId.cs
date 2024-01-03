using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.PlannerTemplates;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<WeekPlannerTemplateId>))]
public record WeekPlannerTemplateId : IStronglyTypedId {
    public Guid Value { get; set; }

    public WeekPlannerTemplateId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<WeekPlannerTemplateId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new WeekPlannerTemplateId(value), mappingHints) {
        }
    }
}