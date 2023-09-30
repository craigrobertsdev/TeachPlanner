using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Entities.WeekPlanners;
public record WeekPlannerId
{
    public Guid Value;

    public WeekPlannerId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<WeekPlannerId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new WeekPlannerId(value), mappingHints)
        {
        }
    }
}
