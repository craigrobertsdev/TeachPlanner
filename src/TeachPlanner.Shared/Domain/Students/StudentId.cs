using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using TeachPlanner.Shared.Contracts;
using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Domain.Students;

[JsonConverter(typeof(StronglyTypedIdJsonConverter<StudentId>))]
public record StudentId : IStronglyTypedId {
    public Guid Value { get; set; }

    public StudentId(Guid value) {
        Value = value;
    }

    public class StronglyTypedIdEfValueConverter : ValueConverter<StudentId, Guid> {
        public StronglyTypedIdEfValueConverter(ConverterMappingHints? mappingHints = null)
            : base(id => id.Value, value => new StudentId(value), mappingHints) {
        }
    }
}