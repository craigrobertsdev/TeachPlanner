using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.Common.Planner;
public record SchoolEventId
{
    public Guid Value;

    public SchoolEventId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<SchoolEventId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new SchoolEventId(value), mappingHints)
        {
        }
    }
}
