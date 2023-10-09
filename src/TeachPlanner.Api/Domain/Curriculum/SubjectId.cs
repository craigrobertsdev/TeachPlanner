using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;
public record SubjectId
{
    public Guid Value;

    public SubjectId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<SubjectId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new SubjectId(value), mappingHints)
        {
        }
    }
}
