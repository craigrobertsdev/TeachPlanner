using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.Teachers;
public record YearDataEntry
{
    public int CalendarYear { get; private set; }
    public YearDataId YearDataId { get; private set; }

    private YearDataEntry(int calendarYear, YearDataId yearDataId)
    {
        CalendarYear = calendarYear;
        YearDataId = yearDataId;
    }

    public static YearDataEntry Create(int calendarYear, YearDataId yearDataId)
    {
        return new(calendarYear, yearDataId);
    }
}
