using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Infrastructure.Persistence.Converters;

public class LessonPlanIdListConverter : ValueConverter<List<Guid>, string>
{
    public LessonPlanIdListConverter() : base(
                l => JsonSerializer.Serialize(l, (JsonSerializerOptions)null!),
                l => JsonSerializer.Deserialize<List<Guid>>(l, (JsonSerializerOptions)null!)!)
    {

    }
}
