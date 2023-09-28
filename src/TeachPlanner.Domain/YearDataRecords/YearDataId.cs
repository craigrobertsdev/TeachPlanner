using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Domain.YearDataRecords;
public record YearDataId
{
    public Guid Value;

    public YearDataId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<YearDataId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new YearDataId(value), mappingHints)
        {
        }
    }
}
