using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.Assessments;

public record AssessmentId
{
    public Guid Value;

    public AssessmentId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<AssessmentId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new AssessmentId(value), mappingHints)
        {
        }

        public StronglyTypedIdEfValueConverter()
            : base(id => id.Value, value => new AssessmentId(value))
        {
        }
    }
}