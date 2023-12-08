using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record WeekPlannerTemplateId {
    public WeekPlannerTemplateId(Guid value) {
        Value = value;
    }

    public Guid Value { get; }

    public class StronglyTypedIdEfValueConverter : ValueConverter<WeekPlannerTemplateId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new WeekPlannerTemplateId(value), mappingHints) {
        }
    }
}