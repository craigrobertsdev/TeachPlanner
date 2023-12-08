using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Domain.Reports;

public record ReportId {
    public Guid Value;

    public ReportId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<ReportId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new ReportId(value), mappingHints) {
        }
    }
}