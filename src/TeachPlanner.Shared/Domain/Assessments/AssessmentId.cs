using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Assessments;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<AssessmentId>))]
public record AssessmentId : IStronglyTypedId {
    public Guid Value { get; set; }

    public AssessmentId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<AssessmentId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new AssessmentId(value), mappingHints) {
        }

        public StronglyTypedIdEfValueConverter()
            : base(id => id.Value, value => new AssessmentId(value)) {
        }
    }
}