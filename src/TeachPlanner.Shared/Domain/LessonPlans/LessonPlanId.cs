using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Shared.Domain.LessonPlans;

public record LessonPlanId {
    public Guid Value;

    public LessonPlanId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<LessonPlanId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new LessonPlanId(value), mappingHints) {
        }
    }
}