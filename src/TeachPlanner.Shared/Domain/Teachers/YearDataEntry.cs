using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Domain.Teachers;

public record YearDataEntry {
    private YearDataEntry(int calendarYear, YearDataId yearDataId) {
        CalendarYear = calendarYear;
        YearDataId = yearDataId;
    }

    public int CalendarYear { get; private set; }
    public YearDataId YearDataId { get; private set; }

    public static YearDataEntry Create(int calendarYear, YearDataId yearDataId) {
        return new YearDataEntry(calendarYear, yearDataId);
    }
}