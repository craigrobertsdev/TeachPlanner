using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;
public record CurriculumSubjectId
{
    public Guid Value;

    public CurriculumSubjectId(Guid value)
    {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<CurriculumSubjectId, Guid>
    {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new CurriculumSubjectId(value), mappingHints)
        {
        }
    }
}
