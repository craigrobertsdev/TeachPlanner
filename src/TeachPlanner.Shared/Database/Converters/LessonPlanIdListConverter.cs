using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Shared.Database.Converters;

public class LessonPlanIdListConverter : ValueConverter<List<Guid>, string> {
    public LessonPlanIdListConverter() : base(
        l => JsonSerializer.Serialize(l, (JsonSerializerOptions)null!),
        l => JsonSerializer.Deserialize<List<Guid>>(l, (JsonSerializerOptions)null!)!) {
    }
}