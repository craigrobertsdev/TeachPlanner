using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Reports;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<ReportId>))]
public record ReportId : IStronglyTypedId {
    public Guid Value { get; set; }

    public ReportId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<ReportId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new ReportId(value), mappingHints) {
        }
    }
}