using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Curriculum;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<SubjectId>))]
public record SubjectId : IStronglyTypedId {
    public Guid Value { get; set; }

    public SubjectId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<SubjectId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new SubjectId(value), mappingHints) {
        }
    }
}