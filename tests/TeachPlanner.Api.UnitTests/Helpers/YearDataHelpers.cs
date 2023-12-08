using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.UnitTests.Helpers;
internal static class YearDataHelpers {
    public static YearData CreateYearData() {
        return YearData.Create(new TeacherId(Guid.NewGuid()), 2023, DayPlanTemplateHelpers.CreateDayPlanTemplate());
    }
}
