using Domain.LessonPlanAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Infrastructure.Persistence.Converters;

public class LessonPlanIdListConverter : ValueConverter<List<LessonPlanId>, string>
{
    public LessonPlanIdListConverter() : base(
                l => JsonSerializer.Serialize(l, (JsonSerializerOptions)null!),
                l => JsonSerializer.Deserialize<List<LessonPlanId>>(l, (JsonSerializerOptions)null!)!)
    {

    }
}
