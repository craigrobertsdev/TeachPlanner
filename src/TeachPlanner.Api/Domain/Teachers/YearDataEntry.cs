using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.Teachers;

public record YearDataEntry
{
    private YearDataEntry(int calendarYear, YearDataId yearDataId)
    {
        CalendarYear = calendarYear;
        YearDataId = yearDataId;
    }

    public int CalendarYear { get; private set; }
    public YearDataId YearDataId { get; private set; }

    public static YearDataEntry Create(int calendarYear, YearDataId yearDataId)
    {
        return new YearDataEntry(calendarYear, yearDataId);
    }
}