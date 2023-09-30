using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Entities.Teachers;
public class YearDataEntry : ValueObject
{
    public int CalendarYear { get; private set; }
    public YearDataId YearDataId { get; private set; }

    public YearDataEntry(int calendarYear, YearDataId yearDataId)
    {
        CalendarYear = calendarYear;
        YearDataId = yearDataId;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CalendarYear;
        yield return YearDataId;
    }
}
